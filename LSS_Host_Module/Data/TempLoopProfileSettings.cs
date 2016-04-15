using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSS_Host_Module.Data
{
    public class TempLoopProfileSettings
    {
        public TempLoopProfileSettings()
        {
            PreHeatTime = 500;
            DwellTime = 1000;
            TargetTemperature = 240;
            TemperatureTolerance = 5;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Pre-heat time, [msec]")]
        [DisplayName("Pre-Heat Time")]
        public int PreHeatTime { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Dwell time, [msec]")]
        [DisplayName("Dwell time")]
        public int DwellTime { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Target temperature, [deg]")]
        [DisplayName("Temperature")]
        public float TargetTemperature { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Temperature tolerance, [deg]")]
        [DisplayName("Temperature tolerance")]
        public float TemperatureTolerance { get; set; }
    }
}
