using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSS_Host_Module.Manager
{
    public interface ITempLoopController
    {
        void Start(int maxPreHeatTime, float targetTemp, int dwellTime, float targetTempTolerance);
        void Stop();
        void SetPID(float Kp, float Ki, float Kd);
        void GetData(out float temp, out float power);
    }
}
