#include <iostream>
#include <string>
#include <time.h>
#include <vector>
#include <thread>
#include <mutex>
#include <conio.h>
#include <atomic>
#include <cstdarg>
#include <sstream>

#include "../include/Logger.h"
#include "../include/TcpClient.h"

// All this could go to a utilities.h lib
#define SERVER_IP "127.0.0.1"
#define SERVER_PORT 8037

//Move
#define KEY_UP 119 // KEY_W
#define KEY_DOWN 115 // KEY_S
#define KEY_LEFT 97 // KEY_A
#define KEY_RIGHT 100 // KEY_D

//Control cannon
#define KEY_FIRE 32 // KEY_SPACE
#define KEY_C_UP 105 // KEY_W
#define KEY_C_DOWN 107 // KEY_S
#define KEY_C_LEFT 106 // KEY_A
#define KEY_C_RIGHT 108 // KEY_D

//Aux
#define KEY_PLAY 112 // KEY_P
#define KEY_EXIT 27 // KEY_ESC

TcpClient tcp_client;

std::string getMove(int move)
{
    switch(move){
        case KEY_PLAY:
            return "Start";
        case KEY_EXIT:
            return "Exit";
        case KEY_UP:
            return "Foward";
        case KEY_DOWN:
            return "Backward";
        case KEY_LEFT:
            return "Left";
        case KEY_RIGHT:
            return "Right";
        case KEY_FIRE:
            return "Fire";
        case KEY_C_UP:
            return "Up";
        case KEY_C_DOWN:
            return "Down";
        case KEY_C_LEFT:
            return "SpinL";
        case KEY_C_RIGHT:
            return "SpinR";
	    default:
            return "ERROR";
    }

}

std::string strFmt(const std::string& format, ...) {
    va_list args;
    va_start(args, format);

    // Determine the required size of the formatted string
    va_list args_copy;
    va_copy(args_copy, args);
    int size = vsnprintf(nullptr, 0, format.c_str(), args_copy);
    va_end(args_copy);

    if (size < 0) {
        va_end(args);
        return "";
    }

    std::vector<char> buffer(size + 1);

    vsnprintf(buffer.data(), buffer.size(), format.c_str(), args);

    va_end(args);

    return std::string(buffer.data());
}

int main(int argc, char *argv[])
{

    TcpClient tcp_client;
    bool game_on = true;
    std::vector<int> myMoves;
    char key;
    int value;
    std::string response;

    Logger& logger = Logger::getInstance();
    logger.log("Logging message...");
    
    logger.log("**------------**------------**------------**\n");
    logger.log("Starting AresGame input\n");

    if (tcp_client.conn(SERVER_IP, SERVER_PORT)){

        logger.log("Connected to AresGame Demo\n");
        
        while (game_on) {

            key = getch();
            value = key;

            logger.log(strFmt("Players is trying to %s", getMove(value).c_str()));
            if(tcp_client.send_data(getMove(value)))
            {
                   // response = tcp_client.receive(512);
                    logger.log(response);
                    if(response == "Finished"){
                        game_on = false;
                    }

            } else {
                    logger.log(strFmt("Error while trying to %s", getMove(value)));
            }

        }

        logger.log("Disconnected to AresGame Demo\n");    
    } else {
        
        logger.log("Error while trying to connect to AresGame Demo\n");    

    }
    
    logger.log("Finishing AresGame input\n");
    return 0;
}