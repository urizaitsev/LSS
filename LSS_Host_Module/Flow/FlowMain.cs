using LSS_Host_Module.Data;
using LSS_Host_Module.Manager;
using LSS_Host_Module.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LSS_Host_Module.Flow
{
    public class FlowMain : Singleton<FlowMain>
    {
        public UIMain UIMainObject { get; set; }
        public DataMain DataMainObject { get; set; }
        public ManagerMain ManagerMainObject { get; set; }

        private Task _tempUpdateTask = null;
        bool _tempLoopTaskCancellationSource = false;
        private Task _tempLoopUpdateTask = null;

        public void Init()
        {
            DataMainObject.Init();
            ManagerMainObject.Init();
            UIMainObject.Init();

            //set defaults
            UIMainObject.TemperatureControlSetMaxTemp(DataMainObject.Data.TemperatureSensor_MaxDisplay);
            UIMainObject.TemperatureControlSetMinTemp(DataMainObject.Data.TemperatureSensor_MinDisplay);
        }

        public void Close()
        {
            ManagerMainObject.Close();
            UIMainObject.Close();
            DataMainObject.Close();
        }

        public FlowMain()
        {
            DataMainObject = new DataMain();

            ManagerMainObject = new ManagerMain();
            ManagerMainObject.HWControllerObject.OnConnectionStatusChanged += HWControllerObject_OnConnectionStatusChanged;

            UIMainObject = new UIMain();
            UIMainObject.OnFileMenu_Exit += UIMainObject_OnFileMenu_Exit;
            UIMainObject.OnFileMenu_Settings += UIMainObject_OnFileMenu_Settings;
            UIMainObject.OnSettingsSaved += UIMainObject_OnSettingsSaved;

            UIMainObject.OnSignalGeneratorChanged += UIMainObject_OnSignalGeneratorChanged;
            UIMainObject.OnSignalGeneratorGetMaximumRange += UIMainObject_OnSignalGeneratorGetMaximumRange;

            UIMainObject.OnTempLoopStarted += UIMainObject_OnTempLoopStarted;

            _tempUpdateTask = Task.Factory.StartNew(
                () =>
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    do
                    {
                        try
                        {
                            if (UIMainObject.SelectedTabIndex != 0)
                            {
                                Thread.Sleep(300);
                            }
                            else
                            {
                                int maxTemp, maxTempIndex;
                                float FPS;
                                long callStart = sw.ElapsedMilliseconds;
                                ManagerMainObject.HWControllerObject.Temp_MLX90621_GetMaxTemperature(out maxTemp, out maxTempIndex, out FPS);
                                long callEnd = sw.ElapsedMilliseconds;
                                double[] temps = new double[4 * 16];
                                for (int i = 0; i < 64; i++)
                                    temps[i] = double.NaN;
                                temps[maxTempIndex] = maxTemp;
                                UIMainObject.TemperatureControlSetTemperature(temps);
                                UIMainObject.TemperatureControlSetFPS(FPS);
                            }
                        }
                        catch (Exception)
                        {

                        }
                    } while (true);
                });
        }

        void UIMainObject_OnTempLoopStarted(TempLoopProfileSettings profileSettings, bool isStarted)
        {
            if ((!isStarted) && (_tempLoopUpdateTask != null))
            {
                //stop controller
                ManagerMainObject.TempLoopController.Stop();

                //stop update task
                _tempLoopTaskCancellationSource = true;
                try
                {
                    _tempLoopUpdateTask.Wait(500);
                }
                catch(Exception)
                {

                }

                _tempLoopUpdateTask = null;
                return;
            }
            else
            {
                //start controller
                ManagerMainObject.TempLoopController.SetPID(DataMainObject.Data.TemperatureLoop_PID_P, DataMainObject.Data.TemperatureLoop_PID_I, DataMainObject.Data.TemperatureLoop_PID_D);
                ManagerMainObject.TempLoopController.Start(profileSettings.PreHeatTime, profileSettings.TargetTemperature, profileSettings.DwellTime, profileSettings.TemperatureTolerance);

                //start update task
                _tempLoopTaskCancellationSource = false;
                _tempLoopUpdateTask = Task.Factory.StartNew(
                () =>
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    do
                    {
                        try
                        {
                            if (UIMainObject.SelectedTabIndex != 1)
                            {
                                Thread.Sleep(300);
                            }
                            else
                            {
                                //update UI
                                float temp, power;
                                ManagerMainObject.TempLoopController.GetData(out temp, out power);
                                UIMainObject.TemperatureLoopAddPoints(DateTime.Now, temp, power);
                            }
                        }
                        catch (Exception)
                        {

                        }
                    } while (!_tempLoopTaskCancellationSource);
                });
            }
        }

        double UIMainObject_OnSignalGeneratorGetMaximumRange(SignalGeneratorControl.AOTypeEnum AOType)
        {
            double value = double.NaN;
            switch(AOType)
            {
                case SignalGeneratorControl.AOTypeEnum.Voltage:
                    value = DataMainObject.Data.LaserDriver_MaxControlVoltage;
                    break;

                case SignalGeneratorControl.AOTypeEnum.Current:
                    value = DataMainObject.Data.LaserDriver_MaxOutputCurrent;
                    break;

                case SignalGeneratorControl.AOTypeEnum.CW:
                    value = DataMainObject.Data.LaserDiode_MaxPower;
                    break;
            }
            return value;
        }

        void UIMainObject_OnSignalGeneratorChanged(SignalGeneratorControl.AOTypeEnum AOType, double amplitude, int period, int iterations, int wave_type, bool isON)
        {
            //calculate value
            int DAC_Value = (int)amplitude;
            switch(AOType)
            {
                case SignalGeneratorControl.AOTypeEnum.Voltage:
                    DAC_Value = (int)amplitude;
                    break;

                case SignalGeneratorControl.AOTypeEnum.Current:
                    DAC_Value = (int)((amplitude / DataMainObject.Data.LaserDriver_MaxOutputCurrent) * DataMainObject.Data.LaserDriver_MaxControlVoltage);
                    break;

                case SignalGeneratorControl.AOTypeEnum.CW:
                    //calculate current value
                    double slopeCurrent = DataMainObject.Data.LaserDiode_MaxCurrent - DataMainObject.Data.LaserDiode_Ith;
                    double driverCurrent = DataMainObject.Data.LaserDiode_Ith + ((amplitude / DataMainObject.Data.LaserDiode_MaxPower) * slopeCurrent);
                    DAC_Value = (int)((driverCurrent / DataMainObject.Data.LaserDriver_MaxOutputCurrent) * DataMainObject.Data.LaserDriver_MaxControlVoltage);
                    break;
            }

            //send command to controller
            bool commandSent = false;
            do
            {
                try
                {
                    ManagerMainObject.HWControllerObject.DAC_MCP4725_SignalGeneratorSetParams(DAC_Value, period, iterations, wave_type, isON);
                    commandSent = true;
                }
                catch(Exception)
                {

                }
            } while (!commandSent);
            

            //start/stop task
            if (isON)
            {
                Task.Factory.StartNew(() =>
                {
                    bool isCycleCompleted = false;
                    DateTime lastLaserCommand = DateTime.Now;
                    do
                    {
                        try
                        {
                            //get temperature
                            /*
                            try
                            {
                                int maxTemp, maxTempIndex;
                                ManagerMainObject.HWControllerObject.Temp_MLX90621_GetMaxTemperature(out maxTemp, out maxTempIndex);
                                double[] temps = new double[4 * 16];
                                for (int i = 0; i < 64; i++)
                                    temps[i] = double.NaN;
                                temps[maxTempIndex] = maxTemp;
                                UIMainObject.TemperatureControlSetTemperature(temps);
                            }
                            catch (Exception)
                            {

                            }
                            */

                            TimeSpan timeSpan = DateTime.Now - lastLaserCommand;
                            if (timeSpan.TotalMilliseconds > 1000)
                            {
                                //Thread.Sleep(1000);
                                lastLaserCommand = DateTime.Now;
                                int ret_amplitude, ret_period, ret_iterations, ret_wave_type;
                                bool ret_isON;
                                ManagerMainObject.HWControllerObject.DAC_MCP4725_SignalGeneratorGetParams(out ret_amplitude, out ret_period, out ret_iterations, out ret_wave_type, out ret_isON);
                                isCycleCompleted = !ret_isON;
                            }    
                        }
                        catch(Exception)
                        {
                            //isCycleCompleted = true;
                        }
                    } while (!isCycleCompleted);

                    //update UI
                    UIMainObject.SignalGeneratorSetState(false);
                });

            }
        }

        void HWControllerObject_OnConnectionStatusChanged(bool connectedState)
        {
            UIMainObject.ShowMainTab = connectedState;
        }

        void UIMainObject_OnSettingsSaved()
        {
            DataMainObject.SaveData();

            //update online variables where applicable

            //Temperature
            UIMainObject.TemperatureControlSetMaxTemp(DataMainObject.Data.TemperatureSensor_MaxDisplay);
            UIMainObject.TemperatureControlSetMinTemp(DataMainObject.Data.TemperatureSensor_MinDisplay);

        }

        void UIMainObject_OnFileMenu_Settings()
        {
            //show dialog
            UIMainObject.ShowSettings(DataMainObject.Data);
        }

        void UIMainObject_OnFileMenu_Exit()
        {
            this.Close();
        }
    }
}
