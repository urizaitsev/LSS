using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LSS_Host_Module.Data
{
    public class AppSettings
    {
        public AppSettings()
        {
            HWController_PortName = "COM3";
            HWController_PollingInterval = 1000;

            LaserDriver_MaxControlVoltage = 5000;
            LaserDriver_MaxOutputCurrent = 180;
            LaserDiode_Ith = 4.62;
            LaserDiode_MaxCurrent = 56.34;
            LaserDiode_MaxPower = 56;

            TemperatureSensor_Type = "MLX90621";
            TemperatureSensor_MaxDisplay = 230;
            TemperatureSensor_MinDisplay = 20;
            TemperatureSensor_ROISize = 3;
            TemperatureSensor_FullFrameGap = 30;

            TemperatureLoop_PID_P = 30;
            TemperatureLoop_PID_I = 10;
            TemperatureLoop_PID_D = 0;
        }

        public string DataFileName
        {
            get
            {
                string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return Path.Combine(exePath, "Data", "MyAppSettings.xml");
            }
        }

        public AppSettings Load()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AppSettings));
            TextReader reader = new StreamReader(DataFileName);
            AppSettings settings = (AppSettings)serializer.Deserialize(reader);
            reader.Close();
            return settings;
        }

        public void Save()
        {
            if (!File.Exists(DataFileName))
            {
                if (!Directory.Exists(Path.GetDirectoryName(DataFileName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(DataFileName));
                }
            }
            XmlSerializer serializer = new XmlSerializer(typeof(AppSettings));
            TextWriter writer = new StreamWriter(DataFileName);
            serializer.Serialize(writer, this);
            writer.Close();
        }
    
        [Browsable(true)]                        
        [ReadOnly(false)]                         
        [Description("Communication port used to connect with HW controller")]            
        [Category("HW Controller")]                 
        [DisplayName("COM Port name")]      
        public string HWController_PortName {get;set;}

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Polling interval [msec] used to connect with HW controller")]
        [Category("HW Controller")]
        [DisplayName("Polling Interval")]
        public int HWController_PollingInterval { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Maximum output current [A] provided using maximum control voltage")]
        [Category("Laser Driver")]
        [DisplayName("Maximum output current")]
        public double LaserDriver_MaxOutputCurrent { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Maximum control voltage [mV] required to provide maximum output current")]
        [Category("Laser Driver")]
        [DisplayName("Maximum control voltage")]
        public double LaserDriver_MaxControlVoltage { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Ith current, [A]")]
        [Category("Laser Diode")]
        [DisplayName("Ith current")]
        public double LaserDiode_Ith { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Maximum diode current, [A] required to provide maximum output CW")]
        [Category("Laser Diode")]
        [DisplayName("Maximum diode current")]
        public double LaserDiode_MaxCurrent { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Maximum diode power, [CW]")]
        [Category("Laser Diode")]
        [DisplayName("Maximum diode power")]
        public double LaserDiode_MaxPower { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Sensor type")]
        [Category("Temperature sensor")]
        [DisplayName("Type")]
        public string TemperatureSensor_Type { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Maximum display temperature temperature")]
        [Category("Temperature sensor")]
        [DisplayName("Max temperature")]
        public double TemperatureSensor_MaxDisplay { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Minimum display temperature temperature, [c]")]
        [Category("Temperature sensor")]
        [DisplayName("Min temperature")]
        public double TemperatureSensor_MinDisplay { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Tracking ROI size, [elements]")]
        [Category("Temperature sensor")]
        [DisplayName("ROI size")]
        public int TemperatureSensor_ROISize { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Gap between 2 ROI 2 full frames")]
        [Category("Temperature sensor")]
        [DisplayName("Full frame gap")]
        public int TemperatureSensor_FullFrameGap { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("PID settings, P")]
        [Category("Temperature loop")]
        [DisplayName("P")]
        public float TemperatureLoop_PID_P { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("PID settings, I")]
        [Category("Temperature loop")]
        [DisplayName("I")]
        public float TemperatureLoop_PID_I { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("PID settings, D")]
        [Category("Temperature loop")]
        [DisplayName("D")]
        public float TemperatureLoop_PID_D { get; set; }
    }
}
