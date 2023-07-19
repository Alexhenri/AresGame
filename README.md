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
