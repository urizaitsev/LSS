using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LSS_Host_Module.Manager
{
    public class SimulatedTempLoopController : ITempLoopController
    {
        public void Start(int maxPreHeatTime, float targetTemp, int dwellTime, float targetTempTolerance)
        {
            _processTaskCancellation = false;
            _processTask = Task.Factory.StartNew(ProcessMethod);
        }

        public void Stop()
        {
            if (_processTask == null)
                return;

            _processTaskCancellation = true;
            _processTask.Wait(500);
            _processTask = null;
        }

        public void SetPID(float Kp, float Ki, float Kd)
        {
            P = Kp;
            I = Ki;
            D = Kd;
        }

        public void GetData(out float temp, out float power)
        {
            Thread.Sleep(30);
            temp = CurrentTemp;
            power = CurrentPower;
        }

        private void ProcessMethod()
        {
            _lastSamplingTimeStamp = new DateTime();
            _lastRamp = 0;
            CurrentPower = 25;
            do
            {
                CalculateSimulatedTemp();
            }while (!_processTaskCancellation);
        }

        private float CurrentTemp { get; set; }
        private float CurrentPower { get; set; }
        private float P { get; set; }
        private float I { get; set; }
        private float D { get; set; }

        private bool _processTaskCancellation = false;
        private Task _processTask = null;

        private const float _maxPowerLUT = 10.0f;
        private const float _maxTimeLUT = 3;
        private float[] _tempLUT = {0,80,130,170,200,215,224,231,237,243,248,253,258,262,266,270,274,278,281,284,286,288,290,291.5f,293,294.5f,296,297,298,299,300};
        private float _lastRamp = 0;
        private DateTime _lastSamplingTimeStamp;

        private void CalculateSimulatedTemp()
        {
            //update current temperature
            DateTime now = DateTime.Now;
            double passedSeconds = (now - _lastSamplingTimeStamp).TotalSeconds;
            float delta = (float)(_lastRamp * passedSeconds);

            //calculate last ramp

            //find index in LUT matching for scaled power
            float scaledFactor = CurrentPower / _maxPowerLUT;
            int closestLUTIndex = -1;
            float closestLUTDiff = float.MaxValue;
            for (int i = 0; i < _tempLUT.Length; i++)
            {
                float scaledLUTValue = _tempLUT[i] * scaledFactor;
                if (Math.Abs(scaledLUTValue - CurrentTemp) < closestLUTDiff)
                {
                    closestLUTIndex = i;
                    closestLUTDiff = Math.Abs(scaledLUTValue - CurrentTemp);
                }
            }

            float nextExpectedTemp = 0;
            if (closestLUTIndex == (_tempLUT.Length - 1))
            {
                //cooling down
                nextExpectedTemp = (_tempLUT[closestLUTIndex]) * scaledFactor;
            }
            else
            {
                //heating up
                nextExpectedTemp = (_tempLUT[closestLUTIndex + 1]) * scaledFactor;
            }

            float secondsPerLUTIndex = _maxTimeLUT / (float)_tempLUT.Length;
            _lastRamp = (nextExpectedTemp - CurrentTemp) / secondsPerLUTIndex; //deg/sec
            
            CurrentTemp += delta;
            _lastSamplingTimeStamp = now;
            Thread.Sleep(10);
        }
    }
}
