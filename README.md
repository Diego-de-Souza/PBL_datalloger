# AtmoTrack: Greenhouse and Datalogger for Motor Winding Drying

## PBL_Datalogger

This project involves the development of a datalogger for the Embedded Systems, Programming Language I, Control and Automation, Solid Mechanics, and Transport Phenomena courses in the Computer Engineering program at FESA - Faculdade de Engenheiro Salvador Arena.

---

## User Manual

This manual outlines all components, materials, and functionalities of the device.

---

## Component Datasheets

Here are the datasheets for the components used in the project:

- [ATMEGA48P](Datasheets/Datasheet-ATMEGA48P.PDF)
- [9V Battery](Datasheets/Datasheet-Bateria9V.pdf)
- [DHT11](Datasheets/Datasheet-DHT11.PDF)
- [Protoboard](Datasheets/Datasheet-Protoboard.pdf)
- [RTCDS1307](Datasheets/Datasheet-RTCDS1307.PDF)
- [SSD1306](Datasheets/Datasheet-SSD1306.PDF)
- [ESP 32 Datasheet.pdf](https://github.com/user-attachments/files/17909496/ESP.32.Datasheet.pdf)
- [LCD 16X2 I2C Datasheet.pdf](https://github.com/user-attachments/files/17909498/LCD.16X2.I2C.Datasheet.pdf)

---

## Purpose

This project was created to enable remote temperature monitoring of motor winding drying greenhouses. The system uses the ESP-32 microcontroller to capture and transmit temperature data to the cloud and the AtmoTrack website.

---

## Required Materials

- **Microcontroller**:
  - ESP-32 (1 unit)
  - Arduino (Atmega 328P) (1 unit)
- **Sensors**:
  - DHT11: Temperature sensor
- **Other Components**:
  - Jumpers, resistors, 16x2 LCD display, 9V battery, and a protoboard (1 unit each)

---

## Assembly Guide

### Connections:

- **DHT11**: Connected to the Arduino.
- **LCD Display**: Connected to the Arduino.
- **ESP-32**: Connected to the Arduino via pin 34.

### Assembly Diagram:

Follow the protoboard diagram to ensure proper connections. It will help organize and ensure the system operates correctly.

---

## System Configuration

### 1. Compiling and Uploading the Code

1. Open the Arduino IDE and connect your ESP-32 and Arduino to the computer via USB cable.
2. Copy the code from the `sketch.ino` file into the Arduino IDE.
3. Ensure all required libraries are installed (use the Library Manager if needed).
4. Compile the code and upload it to the Arduino.
5. Access the AtmoTrack website with your login credentials to register your ESP-32 device and start real-time monitoring of the greenhouse.

---

### 2. How the System Works

#### Initialization:

- Upon opening, the AtmoTrack website displays the homepage. Feel free to explore and learn more about the datalogger.
- To access the dashboards, log in or create an account and select the device you want to monitor.

#### Monitoring:

- **Temperature**: Measured continuously and stored every minute as an average of values collected during the interval.
- **Alerts**: If the temperature goes beyond predefined limits, visual effects (internal ESP-32 LED) are activated.

---

## System Operation

### 1. Data Display

The graph will display the temperature data stored in the cloud, reflecting the real-time temperature of the greenhouse.

### 2. Alerts

If values exceed the set limits:

- **LEDs**: Flash to grab attention.

### 3. Data Storage

The data is stored in the cloud using two separate databases:
- **MongoDB**: Stores temperature-related data.
- **SQL Server**: Stores user, device, and company information.

---

## Simulation Link

Access the project simulation at the following link:  
[https://wokwi.com/projects/414484645399941121](https://wokwi.com/projects/414484645399941121)
