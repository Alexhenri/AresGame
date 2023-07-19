#include "../include/Logger.h"
#include <iostream>
#include <fstream>
#include <chrono>
#include <ctime>

Logger::Logger() {
    logFile.open("AresGameLog.txt");
}

std::string Logger::getCurrentTimestamp() const {
    auto now = std::chrono::system_clock::now();
    std::time_t time = std::chrono::system_clock::to_time_t(now);
    char timestamp[100];
    std::strftime(timestamp, sizeof(timestamp), "%Y-%m-%d %H:%M:%S", std::localtime(&time));
    return timestamp;
}

Logger& Logger::getInstance() {
    static Logger instance;
    return instance;
}

void Logger::log(const std::string& message) {
    std::string timestamp = getCurrentTimestamp();
    logFile << timestamp << " - " << message << std::endl;
}