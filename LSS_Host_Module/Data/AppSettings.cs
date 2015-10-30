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
    }
}
