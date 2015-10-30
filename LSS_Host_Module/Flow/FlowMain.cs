using LSS_Host_Module.Data;
using LSS_Host_Module.Manager;
using LSS_Host_Module.UI;
using System;
using System.Collections.Generic;
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

        public void Init()
        {
            DataMainObject.Init();
            ManagerMainObject.Init();
            UIMainObject.Init();
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
            ManagerMainObject.HWControllerObject.DAC_MCP4725_SignalGeneratorSetParams(DAC_Value, period, iterations, wave_type, isON);

            //start/stop task
            if (isON)
            {
                Task.Factory.StartNew(() =>
                {
                    bool isCycleCompleted = false;
                    do
                    {
                        try
                        {
                            Thread.Sleep(1000);
                            int ret_amplitude, ret_period, ret_iterations, ret_wave_type;
                            bool ret_isON;
                            ManagerMainObject.HWControllerObject.DAC_MCP4725_SignalGeneratorGetParams(out ret_amplitude, out ret_period, out ret_iterations, out ret_wave_type, out ret_isON);
                            isCycleCompleted = !ret_isON;
                        }
                        catch(Exception)
                        {
                            isCycleCompleted = true;
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
