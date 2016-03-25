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
            if (_UIContext != null)
            {
                _UIContext.Send((object state) =>
                {
                    this._settingsForm.Show(data);
                }, null);

            }
        }

        public bool ShowMainTab
        {
            set
            {
                if (_UIContext != null)
                {
                    _UIContext.Send((object state) =>
                    {
                        _mainForm.MainTab.Visible = value;
                    }, null);     

                }
            }
        }

        public int SelectedTabIndex
        {
            get
            {
                int selectedIndex = -1;
                if (_UIContext != null)
                {
                    _UIContext.Send((object state) =>
                    {
                        selectedIndex = _mainForm.MainTab.SelectedIndex;
                    }, null);
                }
                return selectedIndex;
            }
        }

        public event Action OnFileMenu_Exit = delegate { };
        public event Action OnFileMenu_Settings = delegate { };
        public event Action OnSettingsSaved = delegate { };

        public event Action<SignalGeneratorControl.AOTypeEnum, double, int, int, int, bool> OnSignalGeneratorChanged = delegate { };
        public event Func<SignalGeneratorControl.AOTypeEnum, double> OnSignalGeneratorGetMaximumRange = delegate { return 1000; };

        public UIMain()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _mainForm = new MainForm();
            _mainForm.OnFileMenu_Exit += () => OnFileMenu_Exit();
            _mainForm.OnFileMenu_Settings += () => OnFileMenu_Settings();

            _mainForm.SignalGenerator.OnSignalGeneratorChanged +=
                (SignalGeneratorControl.AOTypeEnum AOType, double AO_Amplitude, int AO_Period, int AO_Iterations, int AO_WaveType, bool isStart) =>
                {
                    OnSignalGeneratorChanged(AOType, AO_Amplitude, AO_Period, AO_Iterations, AO_WaveType, isStart);
                };

            _mainForm.SignalGenerator.GetAOMaximum +=
               (SignalGeneratorControl.AOTypeEnum AOType) =>
               {
                   return OnSignalGeneratorGetMaximumRange(AOType);
               };

            _settingsForm = new SettingsForm();
            _settingsForm.OnSettingsSaved += () => OnSettingsSaved();
        }

        public void SignalGeneratorSetState(bool isOn)
        {
            _UIContext.Send((object state) =>
            {
                _mainForm.SignalGenerator.AO_ON = isOn;
            }, null); 
        }

        public void TemperatureControlSetTemperature(double[] temperatures)
        {
            _UIContext.Post((object state) =>
            {
                _mainForm.TempSensor.Temperatures = temperatures;
            }, null); 
        }

        public void TemperatureControlSetFPS(float FPS)
        {
            _UIContext.Post((object state) =>
            {
                _mainForm.TempSensor.FPS = FPS;
            }, null);
        }

        public void TemperatureControlSetMaxTemp(double maxTemp)
        {
            _UIContext.Post((object state) =>
            {
                _mainForm.TempSensor.MaxTemp = maxTemp;
            }, null);
        }

        public void TemperatureControlSetMinTemp(double minTemp)
        {
            _UIContext.Post((object state) =>
            {
                _mainForm.TempSensor.MinTemp = minTemp;
            }, null);
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
