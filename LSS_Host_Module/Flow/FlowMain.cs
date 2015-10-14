using LSS_Host_Module.Data;
using LSS_Host_Module.Manager;
using LSS_Host_Module.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
