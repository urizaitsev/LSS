using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HostController
{
    class Program
    {
        static void Main(string[] args)
        {
            ArduinoBoard arduinoBoard = new ArduinoBoard();
            arduinoBoard.Open("COM3");
            Thread.Sleep(1000);
            arduinoBoard.DAC_MCP4725_SetVoltage(2.0f);
            Thread.Sleep(3000);
            arduinoBoard.Close();
        }
    }
}
