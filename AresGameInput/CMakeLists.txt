cmake_minimum_required(VERSION 3.10)

project(AresGameInput)

# Configuração do C++11 (ou superior)
set(CMAKE_CXX_STANDARD 11)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

# Define uma variável para o caminho completo do diretório raiz do projeto
set(PROJECT_ROOT_DIR ${CMAKE_CURRENT_SOURCE_DIR})

# Lista de arquivos fonte C++ (*.cpp)
set(SOURCES
	${PROJECT_ROOT_DIR}/src/Logger.cpp
	${PROJECT_ROOT_DIR}/src/TcpClient.cpp
	${PROJECT_ROOT_DIR}/src/ares_game.cpp
)

# Lista de arquivos de cabeçalho (*.h)
set(HEADERS
	${PROJECT_ROOT_DIR}/include/Logger.h
   	${PROJECT_ROOT_DIR}/include/TcpClient.h
)

# Executável principal
add_executable(ares_game ${SOURCES} ${HEADERS})
target_link_libraries(ares_game pthread)
target_link_libraries(ares_game ncurses)
