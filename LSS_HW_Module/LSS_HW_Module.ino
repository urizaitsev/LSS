/******************************************************************
Author: Marco Bertschi

Copyright: Marco Bertschi  

Licence: CodeProject Open License [CPOL] 

Version: 2.0

Controller Board: Arduino Mega2560

Written with the official Arduino IDE, AVAILABLE here:
http://www.arduino.cc/en/Main/software

*******************************************************************/
/*   INCLUDES                                                     */
#include <WString.h> //Official Arduino string library
#include <stdlib.h>
/******************************************************************/
/*   CONSTANTS                                                    */
#define LED_TURN_ON_TIMEOUT  1000       //Timeout for LED power time (defines how long the LED stays powered on) in milliseconds
#define LED_PIN 13                      //Pin number on which the LED is connected
#define SERIAL_BAUDRATE 9600            //Baud-Rate of the serial Port
#define COMMAND_DATA_MAX_PARAMS 10      //Maximum parameters available for passing
#define COMMAND_DATA_MESSAGE_LENGTH 63  //Maximum data message length

#define STX "<"                      //ASCII-Code 02, text representation of the STX code
#define ETX ">"                      //ASCII-Code 03, text representation of the ETX code
#define RS  "$"                         //Used as RS code

#define VERSION_NUMBER "1.0"         
#define VERSION_DATE "1.10.2015" 

/*Commands*/
enum Commands {
  Commands_GetVersion = 0,
  Commands_Ping = 1
};

/*Responce errors
/*   WARNING, ERROR AND STATUS CODES                              */
//STATUS
#define MSG_METHOD_SUCCESS 0                      //Code which is used when an operation terminated  successfully
//WARNINGS
#define WRG_NO_SERIAL_DATA_AVAILABLE 250            //Code indicates that no new data is AVAILABLE at the serial input buffer
//ERRORS
#define ERR_RETURN 255
#define ERR_SERIAL_IN_COMMAND_NOT_FORMATTED_1 1   //Code is used when a serial input command is incorrect
#define ERR_SERIAL_IN_COMMAND_NOT_FORMATTED_2 2   //Code is used when a serial input command is incorrect
#define ERR_SERIAL_IN_COMMAND_NOT_FORMATTED_3 3   //Code is used when a serial input command is incorrect
#define ERR_SERIAL_IN_COMMAND_NOT_FORMATTED_4 4   //Code is used when a serial input command is incorrect
#define ERR_SERIAL_IN_COMMAND_NOT_FORMATTED_5 5   //Code is used when a serial input command is incorrect
#define ERR_SERIAL_IN_COMMAND_NOT_FORMATTED_6 6   //Code is used when a serial input command is incorrect
#define ERR_SERIAL_BAD_NUMBER_FORMAT         7   //Code is used when a serial input command is incorrect
/******************************************************************/

/****CLASS DECLARATIONS*********************/
class Flasher
{
	// Class Member Variables
	// These are initialized at startup
	int ledPin;      // the number of the LED pin
	long OnTime;     // milliseconds of on-time
	long OffTime;    // milliseconds of off-time
 
	// These maintain the current state
	int ledState;             		// ledState used to set the LED
	unsigned long previousMillis;  	// will store last time LED was updated
        bool isStarted;
 
  // Constructor - creates a Flasher 
  // and initializes the member variables and state
  public:
  Flasher(int pin, long on, long off)
  {
    ledPin = pin;
    pinMode(ledPin, OUTPUT);     
    	  
    OnTime = on;
    OffTime = off;
    	
    ledState = LOW; 
    previousMillis = 0;
    isStarted = true;
  }
  
  Flasher()
  {
    isStarted = false;
  }
  
  void Start(int pin, long on, long off)
  {
    ledPin = pin;
    pinMode(ledPin, OUTPUT);     
    	  
    OnTime = on;
    OffTime = off;
    	
    ledState = LOW; 
    previousMillis = 0;
    isStarted = true;
  }
  
  void SetParams(long on, long off)
  {
    OnTime = on;
    OffTime = off;
  }
 
  void Update()
  {
    if (!isStarted)
      return;
      
    // check to see if it's time to change the state of the LED
    unsigned long currentMillis = millis();
     
    if((ledState == HIGH) && (currentMillis - previousMillis >= OnTime))
    {
    	ledState = LOW;  // Turn it off
      previousMillis = currentMillis;  // Remember the time
      digitalWrite(ledPin, ledState);  // Update the actual LED
    }
    else if ((ledState == LOW) && (currentMillis - previousMillis >= OffTime))
    {
      ledState = HIGH;  // turn it on
      previousMillis = currentMillis;   // Remember the time
      digitalWrite(ledPin, ledState);	  // Update the actual LED
    }
  }
};

class KeepAlive
{
  Flasher Led1;    // flashing object
  unsigned long LastPingTime;    // milliseconds of off-time
  unsigned long CommunicationTimeOut;
  bool IsConnected;
  bool isStarted;
  public:
  KeepAlive(int ledPin, unsigned long communicationTimeOut)
  {
    Led1.Start(ledPin, 250, 250);
    CommunicationTimeOut = communicationTimeOut;
    IsConnected = false;
	isStarted = true;
  }
  
  KeepAlive()
  {
    IsConnected = false;
	isStarted = false;
  }
  
  void Start(int ledPin, unsigned long communicationTimeOut)
  {
    Led1.Start(ledPin, 250, 250);
    CommunicationTimeOut = communicationTimeOut;
    IsConnected = false;
    isStarted = true;
  }
  
  void Update()
  {
    if (!isStarted)
      return;
      
    Led1.Update();
    if ((millis() - LastPingTime) > CommunicationTimeOut)
    {
      IsConnected = false;
    }
    else
    {
      IsConnected = true;
    }
    
    if (IsConnected)
    {
      Led1.SetParams(1000, 0);
    }
    else
    {
      Led1.SetParams(250, 250);
    }
  }
  
  void PingRecieved()
  {
    LastPingTime = millis();
  }
};

KeepAlive keepAlive;

/*   METHOD DECLARATIONS                                          */
byte readSerialInput(Commands &command, float params[COMMAND_DATA_MAX_PARAMS], byte &paramsCount);
void WriteError(byte code);
void WritePing(float params[COMMAND_DATA_MAX_PARAMS], byte paramsCount);
/******************************************************************/


/*   METHODS    
******************************************************************
The setup method is executed once after the bootloader is done
with his job.
******************************************************************/
void setup(){
  //setup the LED pin for output
  pinMode(LED_PIN, OUTPUT);
  //setup serial pin
  Serial.begin(SERIAL_BAUDRATE);
  //setup keepalive
  keepAlive.Start(LED_PIN, 1000);
}

/*****************************************************************
The loop method is executed forever right after the setup method
is finished.
******************************************************************/
void loop()
{
	keepAlive.Update();
  Commands command;  //Used to store the latest received command
  byte serialResult = 0; //return value for reading operation method on serial in put buffer
  float params[COMMAND_DATA_MAX_PARAMS];
  byte paramsCount;
  serialResult = readSerialInput(command, params, paramsCount);
  if(serialResult == MSG_METHOD_SUCCESS)
  {
    if(command == Commands_GetVersion)//Request for version
    {
        WriteVersion();
    }
    else if(command == Commands_Ping)//Request for ping
    {
        WritePing(params, paramsCount);
		keepAlive.PingRecieved();
    }
  }
  else if(serialResult == WRG_NO_SERIAL_DATA_AVAILABLE)
  {//If there is no data AVAILABLE at the serial port, let the LED blink
     //led1.SetParams(250,250);
  }
  else
  {
    WriteError(serialResult);
  }
}

byte readSerialInput(Commands &command, float params[COMMAND_DATA_MAX_PARAMS], byte &paramsCount)
{
  byte operationStatus = MSG_METHOD_SUCCESS;//Default return is MSG_METHOD_SUCCESS reading data from com buffer.
  paramsCount = 0;
  if (Serial.available() < COMMAND_DATA_MESSAGE_LENGTH) 
  {
    //If not serial input buffer data is AVAILABLE, operationStatus becomes WRG_NO_SERIAL_DATA_AVAILABLE (= No data in the serial input buffer AVAILABLE)
    return WRG_NO_SERIAL_DATA_AVAILABLE;
  }
  
  //check for minimum command length
  /*
    '<' - 1 byte
    command - 1 byte
    '$' - 1 byte
    params count - 1 byte
    '>' - 1 byte
  */
  if (Serial.available() < 5)
    return ERR_SERIAL_IN_COMMAND_NOT_FORMATTED_1;
  
  byte serialInByte;//temporary variable to hold the last serial input buffer character
  bool bIsCommandCompleted = false; //flag - is command string has been completed
  bool bIsTransmissionCompleted = false;
 
  //read initial transmission symbom
  serialInByte = Serial.read();
  if(serialInByte != '<') 
  {
    return ERR_SERIAL_IN_COMMAND_NOT_FORMATTED_2;
  }
  
  //read command
  int commandValue;
  if (ReadIntFromSerial(commandValue) != MSG_METHOD_SUCCESS)
    return ERR_SERIAL_IN_COMMAND_NOT_FORMATTED_3;
    
  command = (Commands)commandValue;
  
  //read number of parameters
  int paramsCountValue = 0;
  if (ReadIntFromSerial(paramsCountValue) != MSG_METHOD_SUCCESS)
    return ERR_SERIAL_IN_COMMAND_NOT_FORMATTED_3;
  paramsCount = (byte)paramsCountValue;
  
  if (paramsCount > 0)
  {
    //read params
    for (int currentParametrIndex=0; currentParametrIndex < paramsCount; currentParametrIndex++)
    {
      float floatValue = 0;
      if (ReadFloatFromSerial(floatValue) != MSG_METHOD_SUCCESS)
        return ERR_SERIAL_IN_COMMAND_NOT_FORMATTED_4;
      params[currentParametrIndex] = floatValue;
    }
  }
  
  //read all empties
  do
  {
    serialInByte = Serial.read();
  }while (Serial.available());
  
  return MSG_METHOD_SUCCESS;
}

/******************************************************************/
bool IsSerialDataReady()
{
  //check if last symbol in buffer is '>'
  
}

/******************************************************************/
byte ReadIntFromSerial(int &value)
{
   if (Serial.available() < 2)
    return ERR_SERIAL_BAD_NUMBER_FORMAT;
    
   char serialInByte;
   bool bIsTransmissionCompleted = false;
   String stringValue = "";
   do
   {
     //Read serial input buffer data byte by byte 
     serialInByte = Serial.read();
     if ((serialInByte == '>') || (serialInByte == '$'))
     {
       //last char, do nothing
       bIsTransmissionCompleted = true;
     }
     else
     {
       stringValue = stringValue + serialInByte;//Add last read serial input buffer byte to stringValue pointer
     }
   }while(!bIsTransmissionCompleted && Serial.available());//until '>','$' comes up or no serial data is available anymore
   value = stringValue.toInt();
   return MSG_METHOD_SUCCESS;
}

/******************************************************************/
byte ReadFloatFromSerial(float &value)
{
  if (Serial.available() < 2)
    return ERR_SERIAL_BAD_NUMBER_FORMAT;
    
   char serialInByte;
   bool bIsTransmissionCompleted = false;
   String stringValue = "";
   do
   {
     //Read serial input buffer data byte by byte 
     serialInByte = Serial.read();
     if ((serialInByte == '>') || (serialInByte == '$'))
     {
       //last char, do nothing
       bIsTransmissionCompleted = true;
     }
     else
     {
       stringValue = stringValue + serialInByte;//Add last read serial input buffer byte to stringValue pointer
     }
   }while(!bIsTransmissionCompleted && Serial.available());//until '>','$' comes up or no serial data is available anymore
   value = stringValue.toFloat();
   return MSG_METHOD_SUCCESS;
}

/******************************************************************/
void WriteError(byte code)
{
  Serial.print(STX);
  Serial.print(ERR_RETURN);
  Serial.print(RS);
  Serial.print(code);
  Serial.print(ETX);
}

/******************************************************************/
void WriteVersion()
{
  Serial.print(STX);
  Serial.print(Commands_GetVersion);
  Serial.print(RS);
  Serial.print(VERSION_NUMBER);
  Serial.print(RS);
  Serial.print(VERSION_DATE);
  Serial.print(ETX);
}

/******************************************************************/
void WritePing(float params[COMMAND_DATA_MAX_PARAMS], byte paramsCount)
{
  char tmp[20];
  Serial.print(STX);
  Serial.print(Commands_Ping);
  for (int currentParametrIndex=0; currentParametrIndex < paramsCount; currentParametrIndex++)
  {
    if (currentParametrIndex == 0)
      Serial.print(RS);
    
    dtostrf(params[currentParametrIndex],10,5,tmp);
    Serial.print(tmp);
    if (currentParametrIndex != (paramsCount - 1))
      Serial.print(RS);
  }
  Serial.print(ETX);
}
