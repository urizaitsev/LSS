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
        }

        void UIMainObject_OnSignalGeneratorChanged(int amplitude, int period, int iterations, int wave_type, bool isON)
        {
            //send command to controller
            ManagerMainObject.HWControllerObject.DAC_MCP4725_SignalGeneratorSetParams(amplitude, period, iterations, wave_type, isON);

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
