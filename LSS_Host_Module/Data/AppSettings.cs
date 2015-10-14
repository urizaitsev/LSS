using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSS_Host_Module.Data
{
    public class AppSettings
    {
        public AppSettings()
        {
            HWController_PortName = "COM3";
            HWController_PollingInterval = 1000;
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
    }
}
