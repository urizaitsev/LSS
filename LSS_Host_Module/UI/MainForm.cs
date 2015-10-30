using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LSS_Host_Module.UI
{
    public partial class MainForm : Form
    {

        public TabControl MainTab
        {
            get
            {
                return tabControlMain;
            }
        }

        public SignalGeneratorControl SignalGenerator
        {
            get
            {
                return signalGeneratorControl;
            }
        }

        public event Action OnFileMenu_Exit = delegate { };
        public event Action OnFileMenu_Settings = delegate { };

        public MainForm()
        {
            InitializeComponent();
        } 

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnFileMenu_Settings();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnFileMenu_Exit();
        }
    }
}
