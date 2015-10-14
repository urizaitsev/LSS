using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LSS_Host_Module.UI
{
    public class UIMain
    {
        private MainForm _mainForm { get; set; }
        private SettingsForm _settingsForm { get; set; }
        private SynchronizationContext _UIContext { get; set; } 

        public void ShowSettings(object data)
        {

            _UIContext.Send((object state) =>
                {
                    this._settingsForm.Show(data);
                }, null);
        }

        public bool ShowMainTab
        {
            set
            {
                _UIContext.Send((object state) =>
                {
                    _mainForm.MainTab.Visible = value;
                }, null);     
            }
        }

        public event Action OnFileMenu_Exit = delegate { };
        public event Action OnFileMenu_Settings = delegate { };
        public event Action OnSettingsSaved = delegate { };

        public UIMain()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _mainForm = new MainForm();
            _mainForm.OnFileMenu_Exit += () => OnFileMenu_Exit();
            _mainForm.OnFileMenu_Settings += () => OnFileMenu_Settings();

            _settingsForm = new SettingsForm();
            _settingsForm.OnSettingsSaved += () => OnSettingsSaved();
        }

        public void Init()
        {
            _UIContext = SynchronizationContext.Current;
            Application.Run(_mainForm);
        }

        public void Close()
        {
            _mainForm.Close();
        }
    }
}
