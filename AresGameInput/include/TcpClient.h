#ifndef CUSTOM_TcpClient_H
#define CUSTOM_TcpClient_H

#include <iostream>    
#include <stdio.h>
#include <string.h>   
#include <string>  
#include <sys/socket.h>   
#include <arpa/inet.h> 
#include <netdb.h>
#include <iostream>

class TcpClient
{
private:
    int sock;
    std::string address;
    int port;
    struct sockaddr_in server;

public:
    TcpClient();
    bool conn(std::string, int);
    bool send_data(std::string data);
    std::string receive(int);
};
#endif