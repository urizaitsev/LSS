using System;
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
            DAC_MCP4725_SetVoltage = 2,
            Error = 255,
        }

        private Task _keepAliveTask { get; set; }
        private bool _isClosing { get; set; }
        private SerialPort _serialPort { get; set; }

        public event Action<bool> OnConnectionStatusChanged = delegate { };
        public bool IsConnected {get;set;}

        public ArduinoBoard()
        {
            _serialPort = new SerialPort();
            IsConnected = false;
        }

        public void Open(string port)
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
            catch(Exception ex)
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

        public void Close()
        {
            if (_serialPort.IsOpen)
                _serialPort.Close();
            _isClosing = true;
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

        public void DAC_MCP4725_SetVoltage(float voltage)
        {
            string[] response = WriteCommand(Commands.DAC_MCP4725_SetVoltage, new float[1] { voltage });
        }

        private string[] WriteCommand(Commands command, float[] parameters)
        {
            if (!_serialPort.IsOpen)
                throw new ApplicationException("Failed to write command, serial port is disconnected");

            int paramsCount = (parameters == null) ? 0 : parameters.Length;
            string commandString = string.Format("{0}{1}{2}{3}", cnstSTX, (int)command, cnstRS, paramsCount);
            if (paramsCount > 0)
            {
                foreach(float parameter in parameters)
                {
                    commandString += cnstRS;
                    commandString += parameter.ToString();
                }
            }
            commandString += cnstETX;
            do
            {
                commandString += " ";
            }while (commandString.Length < cnstMessageLength);
            _serialPort.Write(commandString);
            return ReadCommandResponce(command);
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
