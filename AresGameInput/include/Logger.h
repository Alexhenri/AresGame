#ifndef Logger_H
#define Logger_H

#include <iostream>
#include <fstream>
#include <chrono>
#include <ctime>

class Logger {
private:
    std::ofstream logFile;

    Logger();

    std::string getCurrentTimestamp() const;

public:
    static Logger& getInstance();

    void log(const std::string& message);
};

#endif 