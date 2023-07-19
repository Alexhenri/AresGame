# AresGame
ARES – Software Project – Game Demo

## 1. Description
The idea of the project is to develop and implement a basic 3D scene using the framework Unity3D/C# and another application developed in C++ to interface with the Unity application and control a player inside the scene.

The player inside the game should have the ability to move around the scene with the behavior of a vehicle and contain a weapon system capable of rotating 360º and elevation control.

Upon startup of the scene, targets shall be spawned in random positions of the scenario and move in loops according to predefined movement patterns. The player shall be controlled by the application in C++ and use its weapon system to fire at targets placed in the scene.

Along with the implementation, a document must be written to briefly describe the implementation, classes, messages, and data structures.

## 2. Requirements
This section lists the project requirements, serving as a reference for the implementation.

### AresUnityDemo - Unity3D
- The Unity project shall be named AresUnityDemo.
- The C++ project shall be named AresGameInput.

#### Scene Requirements
- The scene shall contain a plane or a terrain.
- The scene shall have a main camera.
- This application shall exchange messages with AresGameInput.
- The scene ends after a timeout of 2 minutes or when all targets are dead.
- This application shall notify AresGameInput that the game has ended.

#### Targets in the Scene
- The scene shall contain at least 3 prefabs to represent targets for the player.
- Each target shall move according to one of the available movement patterns:
  - Horizontal
  - Circular
  - Sinus wave
- The movement patterns shall be implemented in C# scripts.
- Each target shall keep repeating the configured movement.
- Each target shall be spawned in the scene at random positions.
- The movement pattern for each target shall be configured randomly upon initialization.
- Targets shall die when hit by prefabs fired from the player.
- When a target is in a "dead" state, it should vanish from the scene.

#### Player in the Scene
- The player in the scene shall present movements identical or similar to a vehicle.
- The player shall have a weapon system with the following capabilities:
  - Rotation n x 360º in azimuth
  - Elevation -10º to 60º
  - Shoot the weapon
- Azimuth and elevation rotation of the weapon shall be smooth.

#### Initialization of AresUnityDemo
- The scene shall be empty upon initialization.
- The system shall wait for a start command from AresGameInput.
- After the command, the system shall spawn targets and the player and initialize the game.

### AresGameInput – C++
- This application can run on the same machine as AresUnityDemo or on a different machine connected to a LAN.
- This application shall provide means for the user to input the following commands:
  - Move front/back
  - Move left/right
  - Rotate (azimuth) weapon left/right
  - Elevation up/down
  - Fire weapon
- This application shall send messages to AresUnityDemo containing user inputs.
- This application shall log and save in a text file all messages and data exchanges between AresGameInput and AresUnityDemo.
- The application shall never overwrite a previously stored text log file.
- The log file shall include a timestamp column.
- At the end of the game, the system shall output the elapsed time since the beginning, the number of shots fired, and the number of hits on targets.

### General
- The system must run on a Linux operating system.
- Documentation must be written in English.
- The project shall be added to a Git repository.

# My Demo

## AresUnityDemo

- AresUnityDemo works totally on Unity.
  
  At start:
  - Creates a random number of prefab targets.
  - Creates an environment with:
    - Terrain
    - Directional Light (Default of Unity)
    - Canvas with Crosshair
    - TankPlayer
  - Starts listening to TCP connections (IP: 127.0.0.1, Port: 8037)
  - Log shows what move is getting from AresGameInput
  
  Commands to play:
  1. Enter - Start the game.
  2. Movement of the Tank:
    - W: Forward
    - S: Backward
    - A: Right
    - D: Left
  3. Cannon:
    - Mouse: Control the camera. Left and Right, Up and Down.
    - Space: Fire.
  4. Esc: Exit the game.

## AresGameInput
  
  Build Instructions:
  1. Open a terminal or command prompt.
  2. Navigate to the root directory of the project.
  3. Create a build directory and navigate into it:
     ```
     mkdir build
     cd build
     ```
  4. Generate the build files using CMake:
     ```
     cmake ..
     ```
  5. Build the project:
     ```
     cmake --build .
     ```
  6. The executable should now be generated in the build directory.  
     ```
     ./ares_game
     ```
  
  At start, connects to AresUnityDemo, then send inputs on the keyboard to it.

  (NOT IMPLEMENTED)
  Commands to play:
  1. P - Play the game.
  2. Movement of the Tank:
    - W: Forward
    - S: Backward
    - A: Right
    - D: Left
  3. Cannon Tower:
    - I: Up
    - K: Down
    - J: Spin Right
    - L: Spin Left
    - Space: Fire.

  Log is AresGameLog.txt - The log file is generated in the same directory where the executable was called.

## NOT IMPLEMENTED

- All AresGameInput control over AresUnityDemo. Now, AresUnityDemo can work independently. It can even see which inputs AresGameInput sends, but it doesn't use it.
- Response from AresGameInput to AresUnityDemo.
- End Game Controller. Does not end the game, either by time or by the end of targets.

## POTENTIAL IMPROVEMENTS

- Everything that is not implemented.
- The crosshair to facilitate the aim. Two cameras and a button to change the camera view, that is focusing on the tank, to the cannon, may facilitate the aim. Like a third-person shooter.
- Code, there's always something to improve.
