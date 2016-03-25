using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LSS_Host_Module.UI
{
    public partial class SignalGeneratorControl : UserControl
    {
        public enum AOTypeEnum
        {
            Voltage,
            Current,
            CW
        };

        public SignalGeneratorControl()
        {
            InitializeComponent();

            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                //AO
                comboBoxAOType.Items.AddRange(new object[] { "Voltage", "Current", "CW"});
                radioButtonWaveType_DC.Checked = true;
                AO_Type = AOTypeEnum.Voltage;
                AO_Amplitude = 0;
                AO_Period = 10;
                AO_Iterations = 1;
                AO_WaveType = 1;
            }
        }

        public event Action<AOTypeEnum, double, int, int, int, bool> OnSignalGeneratorChanged = delegate { };
        public Func<AOTypeEnum, double> GetAOMaximum = delegate { return 1000; }; 

        public AOTypeEnum AO_Type
        {
            get
            {
                AOTypeEnum returnedType = AOTypeEnum.Voltage;
                if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
                {
                    switch (comboBoxAOType.SelectedIndex)
                    {
                        case 0:
                            returnedType = AOTypeEnum.Voltage;
                            break;

                        case 1:
                            returnedType = AOTypeEnum.Current;
                            break;

                        case 2:
                            returnedType = AOTypeEnum.CW;
                            break;
                    }
                }
                return returnedType;
            }
            set
            {
                if ((System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime) && (comboBoxAOType.Items != null) && (comboBoxAOType.Items.Count > 0))
                {
                    switch (value)
                    {
                        case AOTypeEnum.Voltage:
                            comboBoxAOType.SelectedIndex = 0;
                            break;

                        case AOTypeEnum.Current:
                            comboBoxAOType.SelectedIndex = 1;
                            break;

                        case AOTypeEnum.CW:
                            comboBoxAOType.SelectedIndex = 2;
                            break;
                    }
                }
            }
        }

        public double AO_Amplitude
        {
            set
            {
                double maximumValue = GetAOMaximum(AO_Type);
                if (AO_Amplitude != value)
                {
                    trackBarAOAmplitude.Value = (int)(value * (double)(trackBarAOAmplitude.Maximum / maximumValue));
                }
                labelAOValue.Text = GetAOLabel(AO_Amplitude);
            }
            get
            {
                double maximumValue = GetAOMaximum(AO_Type);
                double AOValue = (double)((double)trackBarAOAmplitude.Value / (double)(trackBarAOAmplitude.Maximum / maximumValue));
                return AOValue;
            }
        }

        public int AO_Period
        {
            set
            {
                const double MaxPeriodValue = 10000.0;
                const double LinearPeriodLimit = 100;
                if (AO_Period != value)
                {
                    if (trackBarAOPeriod.Value < LinearPeriodLimit)
                    {
                        trackBarAOPeriod.Value = value;
                    }
                    else
                    {
                        trackBarAOPeriod.Value = (int)((double)(value - LinearPeriodLimit) * (double)((trackBarAOPeriod.Maximum - LinearPeriodLimit) / MaxPeriodValue) + LinearPeriodLimit);
                    }
                }
                labelAOPeriodValue.Text = string.Format("{0} [msec]", AO_Period.ToString());
            }
            get
            {
                const double MaxPeriodValue = 10000.0;
                const double LinearPeriodLimit = 100;

                int PeriodValue = 0;
                if (trackBarAOPeriod.Value < LinearPeriodLimit)
                {
                    PeriodValue = trackBarAOPeriod.Value;
                }
                else
                {
                    PeriodValue = (int)((double)(trackBarAOPeriod.Value - LinearPeriodLimit) / (double)((trackBarAOPeriod.Maximum - LinearPeriodLimit) / MaxPeriodValue) + LinearPeriodLimit);
                }
                return PeriodValue;
            }
        }

        public int AO_Iterations
        {
            get
            {
                int IterationsValue = trackBarAOIterations.Value;
                if (IterationsValue == trackBarAOIterations.Maximum)
                    IterationsValue = 100;
                return IterationsValue;
            }
            set
            {
                if (AO_Iterations != value)
                {
                    if (value < trackBarAOIterations.Maximum)
                    {
                        trackBarAOIterations.Value = value;
                    }
                    else
                    {
                        trackBarAOIterations.Value = trackBarAOIterations.Maximum;
                    }
                }
                labelAOIterationsValue.Text = AO_Iterations.ToString();
            }
        }

        public int AO_WaveType
        {
            get
            {
                if (radioButtonWaveType_DC.Checked)
                    return 0;
                else if (radioButtonWaveType_Square.Checked)
                    return 1;
                else if (radioButtonWaveType_Saw1.Checked)
                    return 2;
                else if (radioButtonWaveType_Saw2.Checked)
                    return 3;
                else if (radioButtonWaveType_Saw3.Checked)
                    return 4;
                else
                    return -1;
            }
            set
            {
                if (AO_WaveType != value)
                {
                    switch (value)
                    {
                        case 0: //DC
                            radioButtonWaveType_DC.Checked = true;
                            break;
                        case 1:
                            radioButtonWaveType_Square.Checked = true;
                            break;
                        case 2:
                            radioButtonWaveType_Saw1.Checked = true;
                            break;
                        case 3:
                            radioButtonWaveType_Saw2.Checked = true;
                            break;
                        case 4:
                            radioButtonWaveType_Saw3.Checked = true;
                            break;
                    }
                }
            }
        }

        public bool AO_ON
        {
            get
            {
                return checkBoxStartStop.Checked;
            }
            set
            {
                if (value == checkBoxStartStop.Checked)
                    return;
                checkBoxStartStop.Checked = value;
            }
        }

        private void trackBarAO_ValueChanged(object sender, EventArgs e)
        {
            double maximumValue = GetAOMaximum(AO_Type);
            double AOValue = (double)((double)trackBarAOAmplitude.Value / (double)(trackBarAOAmplitude.Maximum / maximumValue));
            labelAOValue.Text = GetAOLabel(AOValue);
        }

        private void trackBarAOPeriod_ValueChanged(object sender, EventArgs e)
        {
            const double MaxPeriodValue = 10000.0;
            const double LinearPeriodLimit = 100;
            double PeriodValue = 0;
            if (trackBarAOPeriod.Value < LinearPeriodLimit)
            {
                PeriodValue = trackBarAOPeriod.Value;
            }
            else
            {
                PeriodValue = (double)(trackBarAOPeriod.Value - LinearPeriodLimit) / (double)((trackBarAOPeriod.Maximum - LinearPeriodLimit) / MaxPeriodValue) + LinearPeriodLimit;
            }

            labelAOPeriodValue.Text = string.Format("{0} [msec]", PeriodValue.ToString());
        }

        private void trackBarAOIterations_ValueChanged(object sender, EventArgs e)
        {
            int IterationsValue = trackBarAOIterations.Value;
            if (IterationsValue == trackBarAOIterations.Maximum)
                IterationsValue = 100;
            labelAOIterationsValue.Text = IterationsValue.ToString();
        }

        private void checkBoxStartStop_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxStartStop.Checked)
            {
                checkBoxStartStop.Text = "Stop";
            }
            else
            {
                checkBoxStartStop.Text = "Start";
            }

            radioButtonWaveType_DC.Enabled = !checkBoxStartStop.Checked;
            radioButtonWaveType_Square.Enabled = !checkBoxStartStop.Checked;
            radioButtonWaveType_Saw1.Enabled = !checkBoxStartStop.Checked;
            radioButtonWaveType_Saw2.Enabled = !checkBoxStartStop.Checked;
            radioButtonWaveType_Saw3.Enabled = !checkBoxStartStop.Checked;
            trackBarAOAmplitude.Enabled = !checkBoxStartStop.Checked;
            trackBarAOPeriod.Enabled = !checkBoxStartStop.Checked;
            trackBarAOIterations.Enabled = !checkBoxStartStop.Checked;
            //notify host
            OnSignalGeneratorChanged(AO_Type, AO_Amplitude, AO_Period, AO_Iterations, AO_WaveType, checkBoxStartStop.Checked);
        }

        private string GetAOLabel(double value)
        {
            string units = string.Empty;
            switch(AO_Type)
            {
                case AOTypeEnum.Voltage:
                    units = "[mV]";
                    break;

                case AOTypeEnum.Current:
                    units = "[A]";
                    break;

                case AOTypeEnum.CW:
                    units = "[CW]";
                    break;
            }
            return string.Format("{0} {1}", value, units);
        }

        private void comboBoxAOType_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelAOValue.Text = GetAOLabel(AO_Amplitude);
        }
    }
}
