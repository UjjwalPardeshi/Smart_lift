# Smart Lift Occupancy Detection and Visualization System for College Campus

This project proposes an innovative solution to address the challenge of managing lift crowds in college campuses. By leveraging existing camera infrastructure and advanced technology, our tool utilizes OpenVINO person detection models and Unity3D app development to create an interactive phone application. This application offers real-time visualization of lift crowds using stylized 3D models of students, enabling users to make informed decisions about elevator usage.

## Features

- Utilizes Raspberry Pi as a server for running the OpenVINO model for detecting the number of people present in the camera frame.
- Processes real-time input received by IP cameras (acting as security cameras of SRMIST) and sends the output of the number of people detected to our server using sockets.
- Completely wireless solution, easily accessible within SRM University via the app by connecting to SRMIST WiFi.

## Screenshots
![System Archeitecture diagram](![image](![image](https://github.com/UjjwalPardeshi/realtime_persondetection/assets/113883490/95c0a835-ba92-4949-a29d-b7278957bf52))

![Realtime detection on Crowd_1](![image](https://github.com/UjjwalPardeshi/realtime_persondetection/assets/113883490/6d6a4f38-b6bd-4406-a08f-bcc3fe1cac17)


![Realtime detection on Crowd_2](https://github.com/UjjwalPardeshi/realtime_persondetection/assets/113883490/c1f535b3-b71d-4e8b-9220-4402c1252607)

![Raspberry Pi connected with SSH running our OpenVINO model](https://github.com/UjjwalPardeshi/realtime_persondetection/assets/113883490/3f878bbf-3a96-4b6b-bb3f-de16d493f2a6)

![Sending output to the app with the help of sockets](https://github.com/UjjwalPardeshi/realtime_persondetection/assets/113883490/34e54991-f61f-49af-9c92-d4dd492a1599)

![App Output of Crowd 1](https://github.com/UjjwalPardeshi/realtime_persondetection/assets/113883490/15a22b67-0cf3-4d3b-8a15-665a81ee8b50)

![App Output of Crowd 2](https://github.com/UjjwalPardeshi/realtime_persondetection/assets/113883490/b069e585-5e73-4faf-8ff0-7a5faf744de1)

## How to Use

1. Clone the repository.
2. Install the necessary dependencies.
3. Run the server code on Raspberry Pi.
4. Connect the IP cameras and ensure they are capturing the desired area.
5. Connect the app to SRMIST WiFi to access real-time lift occupancy information.
