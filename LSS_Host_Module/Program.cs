using LSS_Host_Module.Flow;
using LSS_Host_Module.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LSS_Host_Module
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            FlowMain.Instance.Init();
        }
    }
}
