#include <iostream>
#include <sys/types.h>
#include <sys/socket.h>
#include <netdb.h>
#include <string.h>
#include <arpa/inet.h>
#include <unistd.h>
#include <string>
#include <fstream>
#include <stdlib.h>
#include <time.h>
#include <stdio.h>
using namespace std;
int main(){


        int listeningS = socket(AF_INET, SOCK_STREAM, 0);
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

        while(bufS != NULL){
                if(strcmp(bufS, "save") == 0){
                        save = true;
                        addToList.open("Keys/listofusers.txt", ios::in);
                }
                if(save == true && counter ==1){
                        saveKey.open("Keys/" + string(bufS) + ".txt", ios::out | ios::app);
                        string line;
                        while(getline(addToList, line))
                                if(line == string(bufS))add = false;


                        if(add == true){
                                addToList.close();
                                addToList.open("Keys/listofusers.txt", ios::out | ios::app);
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
                        fetchKey.open("Keys/"+ string(bufS) + ".txt", ios::in);
                        if(fetchKey){

                                string line;
                                int count = 0;;
                                while(getline(fetchKey, line))count++;
                                srand(time(NULL));
                                int random = rand() % count +1;
                                line = "";

                                fetchKey.close();

                                fetchKey.open("Keys/"+ string(bufS) + ".txt", ios::in);
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
}
        close(listeningS);
        return 0;
}