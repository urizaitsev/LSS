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
    public partial class SettingsForm : Form
    {

        public event Action OnSettingsSaved = delegate { }; 

        public SettingsForm()
        {
            InitializeComponent();
        }

        public void Show(Object data)
        {
            this.propertyGrid.SelectedObject = data;
            this.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OnSettingsSaved();
            this.Hide();
        }
    }
}
