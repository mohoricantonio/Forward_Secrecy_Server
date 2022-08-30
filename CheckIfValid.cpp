#include <iostream>
#include <string.h>
#include <unistd.h>
#include <string>
#include <fstream>
#include <stdlib.h>
#include <time.h>
#include <stdio.h>
#include <ctime>

using namespace std;

bool IsValid(string file, string now);
void delete_line(const char *file_name, int n);

int main(){
        ifstream list;
        ifstream user;
        string userFile;
        time_t now;
        struct tm *ptr;
        int lineCounter;
        int fileCounter;
        string file_name;

while(true){
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
}
        return 0;
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