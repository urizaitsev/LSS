﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LSS_Host_Module.Manager
{
    public class ArduinoBoard
    {
        private const string cnstSTX = "<";
        private const string cnstETX = ">";
        private const string cnstRS = "$";
        private const int cnstTimeOut = 1000;
        private const int cnstMessageLength = 63;

        private enum Commands
        {
            GetVersion = 0,
            Ping = 1,
            DAC_MCP4725_SignalGeneratorSetParams = 2,
            DAC_MCP4725_SignalGeneratorGetParams = 3,
            Temp_MLX90621_SetParams = 4,
            Temp_MLX90621_GetMaxTemperature = 5,
            Temp_Loop_SetPID = 6,
            Temp_Loop_Start = 7,
            Temp_Loop_Stop = 8,
            Error = 255,
        }

        private Task _keepAliveTask { get; set; }
        private Task _OnConnectedTask { get; set; }
        private bool _isClosing { get; set; }
        private SerialPort _serialPort { get; set; }
        private ManualResetEvent _communicationLock;
        private Object _lock;

        public event Action<bool> OnConnectionStatusChanged = delegate { };
        public bool IsConnected {get;set;}

        public ArduinoBoard()
        {
            _serialPort = new SerialPort();
            IsConnected = false;
            _communicationLock = new ManualResetEvent(true);
            _lock = new object();
        }

        public void Open(string port)
        {
            _OnConnectedTask = Task.Factory.StartNew(() =>
                {
                    do
                    {
                        try
                        {
                            Thread.Sleep(1000);
                            if (!IsConnected)
                            {
                                TryOpen(port);
                            }
                        }
                        catch(Exception)
                        {
                            OnConnectionStatusChanged(false);
                            Thread.Sleep(5000);
                        }
                        
                    } while (!_isClosing);
                });
        }

        public void Close()
        {
            if (_serialPort.IsOpen)
                _serialPort.Close();
            _isClosing = true;

            if (_OnConnectedTask != null)
                _OnConnectedTask.Wait(10000);
            if (_keepAliveTask != null)
                _keepAliveTask.Wait(2000);
        }

        public void GerVersion(out Version version, out DateTime dateTime)
        {
            string[] returnData = WriteCommand(Commands.GetVersion, null);
            version = new Version(returnData[0]);
            dateTime = DateTime.Parse(returnData[1]);
        }

        public void Ping()
        {
            string[] returnData = WriteCommand(Commands.Ping, new float[4] { 100.0f, 200.0f, 0.1456f, 2687.145678f });
        }

        public void DAC_MCP4725_SignalGeneratorSetParams(int amplitude, int period, int iterations, int wave_type, bool isON)
        {
            string[] response = WriteCommand(Commands.DAC_MCP4725_SignalGeneratorSetParams, new float[5] { (float)amplitude, (float)period, (float)iterations, (float)wave_type, isON? 1 : -1 });
        }

        public void DAC_MCP4725_SignalGeneratorGetParams(out int amplitude, out int period, out int iterations, out int wave_type, out bool isON)
        {
            amplitude = 0;
            period = 0;
            iterations = 0;
            wave_type = 0;
            isON = false;
            string[] response = WriteCommand(Commands.DAC_MCP4725_SignalGeneratorGetParams, new float[0]);
            amplitude = (int)float.Parse(response[0]);
            period = (int)float.Parse(response[1]);
            iterations = (int)float.Parse(response[2]);
            wave_type = (int)float.Parse(response[3]);
            isON =      float.Parse(response[4]) > 0 ? true : false;
        }

        public void Temp_MLX90621_SetParams(int ROI_Size, int FullFramesGap)
        {
            string[] response = WriteCommand(Commands.Temp_MLX90621_SetParams, new float[2] { ROI_Size, FullFramesGap });
        }

        public void Temp_MLX90621_GetMaxTemperature(out int maxTemperature, out int maxTemperatureIndex, out float FPS)
        {
            string[] response = WriteCommand(Commands.Temp_MLX90621_GetMaxTemperature, new float[0]);
            maxTemperature = (int)float.Parse(response[0]);
            maxTemperatureIndex = (int)float.Parse(response[1]);
            FPS = float.Parse(response[2]);
        }

        public void Temp_Loop_SetPID(float Kp, float Ki, float Kd)
        {
            string[] response = WriteCommand(Commands.Temp_Loop_SetPID, new float[3] { Kp, Ki, Kd });
        }

        public void Temp_Loop_Stop()
        {
            string[] response = WriteCommand(Commands.Temp_Loop_SetPID, new float[0] { });
        }
        
        public void Temp_Loop_Start(int maxPreHeatTime, float preHeatRate, float targetTemp, int dwellTime, float targetTempTolerance)
        {
            string[] response = WriteCommand(Commands.Temp_Loop_Start, new float[5] { maxPreHeatTime, preHeatRate, targetTemp, dwellTime, targetTempTolerance });
        }
 

        private void TryOpen(string port)
        {
            _isClosing = false;
            IsConnected = false;
            //check if port is opened
            if (_serialPort.IsOpen)
            {
                throw new ApplicationException("Failed to open serial port, it is already being used");
            }

            //try to open
            try
            {
                _serialPort.PortName = port;
                _serialPort.Open();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to open serial port", ex);
            }

            //get version
            Version version;
            DateTime dateTime;
            GerVersion(out version, out dateTime);

            //start keep-alive thread
            _keepAliveTask = Task.Factory.StartNew(
            () =>
            {
                do
                {
                    try
                    {
                        Ping();
                        bool oldConnectedStatus = IsConnected;
                        IsConnected = true;
                        if (oldConnectedStatus != IsConnected)
                            OnConnectionStatusChanged(IsConnected);
                        Thread.Sleep(1000);
                    }
                    catch (Exception)
                    {
                        bool oldConnectedStatus = IsConnected;
                        IsConnected = false;
                        if (oldConnectedStatus != IsConnected)
                            OnConnectionStatusChanged(IsConnected);
                    }
                } while (!_isClosing);

            });
        }

        private string[] WriteCommand(Commands command, float[] parameters)
        {
            try
            {
                lock (_lock)
                {
                    _communicationLock.WaitOne(cnstTimeOut);
                    _communicationLock.Reset();
                    if (!_serialPort.IsOpen)
                        throw new ApplicationException("Failed to write command, serial port is disconnected");

                    int paramsCount = (parameters == null) ? 0 : parameters.Length;
                    string commandString = string.Format("{0}{1}{2}{3}", cnstSTX, (int)command, cnstRS, paramsCount);
                    if (paramsCount > 0)
                    {
                        foreach (float parameter in parameters)
                        {
                            commandString += cnstRS;
                            commandString += parameter.ToString();
                        }
                    }
                    commandString += cnstETX;
                    do
                    {
                        commandString += " ";
                    } while (commandString.Length < cnstMessageLength);
                    _serialPort.Write(commandString);
                    return ReadCommandResponce(command);
                }  
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                _communicationLock.Set();
            }
        }

        /// <summary>
        /// Read responce for specififc command
        /// The responce occur within specififc timeout
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private string[] ReadCommandResponce(Commands command)
        {
            DateTime startTime = DateTime.Now;
            bool isTimeOut = false;
            do
            {
                Thread.Sleep(10);
                if ((DateTime.Now - startTime).TotalMilliseconds > cnstTimeOut)
                    isTimeOut = true;
            } while (!isTimeOut && (_serialPort.BytesToRead == 0));

            if (isTimeOut)
                throw new ApplicationException("Failed to get reply for command:" + command);

            string data = _serialPort.ReadTo(cnstETX);
            //Split into fields
            string[] dataArray = data.Split(new string[] { cnstSTX, cnstRS }, StringSplitOptions.RemoveEmptyEntries);

            //verify first field
            Commands respondedCommand = (Commands)Enum.Parse(typeof(Commands), dataArray[0]);

            //check if not error
            if (respondedCommand == Commands.Error)
                throw new ApplicationException(string.Format("Command {0} returned error {1}", command, dataArray[1]));

            if (respondedCommand != command)
                throw new ApplicationException("Wrong responce for command:" + command);

            //return data fields only
            string[] responseData = new string[dataArray.Length - 1];
            for (int i = 0; i < responseData.Length; i++)
                responseData[i] = dataArray[i + 1];
            return responseData;
        }
    }
}
