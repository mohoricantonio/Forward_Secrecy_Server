#include <iostream>
#include <sys/types.h>
#include <sys/socket.h>
#include <netdb.h>
#include <string.h>
#include <string>
#include <arpa/inet.h>
#include <unistd.h>
#include <string>
#include <fstream>
#include <stdlib.h>
#include <time.h>
#include <ctime>
#include <stdio.h>
#include <thread>
#include <csignal>
using namespace std;

bool IsValid(string file, string now);
void delete_line(const char *file_name, int n);
void *CheckIfValid(void *x);
void end (int sig);
pthread_t *Check_t = new pthread_t;
int listeningS;
pthread_mutex_t mutex;

int main(){

        listeningS = socket(AF_INET, SOCK_STREAM, 0);
        if(listeningS == -1){
                cout<< "Can't create a socket!"<<endl;
                return -1;
        }

        sockaddr_in hint;
        hint.sin_family = AF_INET;
        hint.sin_port = htons(54000);
        inet_pton(AF_INET, "0.0.0.0", &hint.sin_addr);

        if(bind(listeningS, (sockaddr *)&hint, sizeof(hint)) == -1){
                cout<< "Can't bind socket!"<<endl;
                return -2;
        }

        if(listen(listeningS, SOMAXCONN) == -1){
                cout<< "Can't listen!"<<endl;
                return -3;
        }

        sockaddr_in client;
        socklen_t clientSize = sizeof(client);

        sigset(SIGINT, end);
        pthread_mutex_init(&mutex, NULL);
        pthread_create (Check_t, NULL, CheckIfValid, NULL);


while(true){

        int clientSocket = accept(listeningS, (sockaddr *)&client, &clientSize);

        if(clientSocket == -1){
                cout<< "Problem with client connecting"<< endl;
                return -4;
        }

        char buf[4096];

        memset (buf, 0, 4096);
        int bytesRecv = recv(clientSocket, buf, 4096, 0);

        if(bytesRecv == -1){
                cout<< "There was a connection issue!" <<endl;
        }
        
        char *bufS = strtok(buf, "!/&");
        bool save = false;
        bool fetch = false;
        int counter = 0;
        fstream saveKey;
        fstream addToList;
        fstream fetchKey;
        bool add = true;
        char sendMessage[4096];
        memset (sendMessage, 0, 4096);
        bool found = true;

        pthread_mutex_lock(&mutex);

        while(bufS != NULL){
                if(strcmp(bufS, "save") == 0){
                        save = true;
                        addToList.open("listofusers.txt", ios::in);
                }
                if(save == true && counter ==1){
                        saveKey.open( string(bufS) + ".txt", ios::out | ios::app);
                        string line;
                        while(getline(addToList, line))
                                if(line == string(bufS))add = false;


                        if(add == true){
                                addToList.close();
                                addToList.open("listofusers.txt", ios::out | ios::app);
                                addToList<<string(bufS) + "\n";
                        }
                        addToList.close();

                }
                if(save == true && counter ==2){
                        saveKey<<string(bufS) +"!/&";
                }
                if(save == true && counter == 3){
                        saveKey<<string(bufS) + "\n";
                        saveKey.close();
                }


                if(strcmp(bufS, "fetch")==0){
                        fetch = true;
                }
                if(fetch == true && counter ==1){
                        fetchKey.open(string(bufS) + ".txt", ios::in);
                        if(fetchKey){

                                string line;
                                int count = 0;;
                                while(getline(fetchKey, line))count++;
                                srand(time(NULL));
                                int random = rand() % count +1;
                                line = "";

                                fetchKey.close();

                                fetchKey.open(string(bufS) + ".txt", ios::in);
                                for(int i = 0; i< random; i++) getline(fetchKey,line);
                                fetchKey.close();

                                int l = line.length();
                                char fetchedKey[l+1];
                                memset(fetchedKey, 0, l+1);
                                strcpy(fetchedKey, line.c_str());

                                char *f = strtok(fetchedKey, "!/&");
                                strcpy(sendMessage, f);
                        }
                }
                counter++;
                bufS= strtok(NULL, "!/&");
        }

        if(save == true) strcpy (sendMessage, "Key saved!");
        else if(fetch == true && strcmp (sendMessage, "") == 0) strcpy (sendMessage, "Requested user was not found");
        else if(fetch != true) strcpy (sendMessage, "Bad request");

        send(clientSocket, sendMessage, sizeof(sendMessage) + 1, 0);
        close(clientSocket);
        pthread_mutex_unlock(&mutex);

}
        close(listeningS);
        return 0;
}

void *CheckIfValid(void *x){

        ifstream list;
        ifstream user;
        string userFile;
        time_t now;
        struct tm *ptr;
        int lineCounter;
        int fileCounter;
        string file_name;

while(true){

        sleep(1);
        pthread_mutex_lock(&mutex);
        list.open("listofusers.txt");

        fileCounter = 0;

        while(getline(list, userFile)){
                fileCounter++;
                file_name = "";
                user.open(userFile + ".txt");
                string line;

                lineCounter = 0;

                while(getline(user, line)){

                        lineCounter++;
                        int l = line.length();
                        char date[l+1];
                        memset(date, 0, l+1);
                        strcpy(date, line.c_str());

                        char *d = strtok(date, "!/&");
                        d = strtok(NULL, "!/&");

                        now = time(0);
                        char now_str[256];
                        ptr = localtime(&now);

                        strftime(now_str, sizeof(now_str), "%d.%m.%Y. %H:%M:%S", ptr);

                        if(!IsValid(d, now_str)){

                                string file_name = userFile + ".txt";
                                delete_line(file_name.c_str(), lineCounter);
                                lineCounter--;
                        }

                }

                user.close();
                string file_name = userFile + ".txt";

                if(lineCounter == 0){

                        delete_line("listofusers.txt", fileCounter);
                        fileCounter--;
                        remove(file_name.c_str());
                }

        }
        list.close();

        pthread_mutex_unlock(&mutex);
        }
}

void delete_line(const char* file_name, int n){
        ifstream is(file_name);

        ofstream ofs;
        ofs.open("temp.txt");

        string line;
        int line_no = 1;


        while (getline(is, line))
        {
                if(n == line_no){
                        line_no++;
                        continue;
                }
                else{
                        ofs << line <<endl;
                        line_no++;
                }
        }

        ofs.close();
        is.close();

        remove(file_name);

        rename("temp.txt", file_name);
}

bool IsPastTime(int hourF, int minF, int secF, int hourN, int minN, int secN){
        if(hourF < hourN) return true;
        else if(hourF == hourN){
                if(minF < minN) return true;
                else if(minF == minN){
                        if(secF < secN) return true;
                                                 }
        }
        return false;
}

bool IsValid(string file, string now){

        int dayF = stoi(file.substr(0,2));
        int monthF = stoi(file.substr(3,2));
        int yearF = stoi(file.substr(6,4));
        int hourF = stoi(file.substr(12,2));
        int minF = stoi(file.substr(15,2));
        int secF = stoi(file.substr(18,2));

        int dayN = stoi(now.substr(0,2));
        int monthN = stoi(now.substr(3,2));
        int yearN = stoi(now.substr(6,4));
        int hourN = stoi(now.substr(12,2));
        if(hourN == 22) hourN = 0;
        else if(hourN == 23) hourN = 1;
        else hourN = hourN +2;

        int minN = stoi(now.substr(15,2));
        int secN = stoi(now.substr(18,2));

        if(yearF < yearN) return false;
        else if(yearF == yearN){
                if(monthF < monthN) return false;
                else if(monthF == monthN){
                        if(dayF < dayN) return false;
                        else if(dayF == dayN){
                                if(IsPastTime(hourF, minF, secF, hourN, minN, secN)) return false;
                        }
                }
        }

        return true;
}

void end(int sig){

        pthread_kill(*Check_t, SIGKILL);
        pthread_mutex_destroy(&mutex);

        close(listeningS);

        exit(0);
}
