using LSS_Host_Module.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSS_Host_Module.Manager
{
    public class ManagerMain
    {

        public ArduinoBoard HWControllerObject { get; set; }

        public ManagerMain()
        {
            HWControllerObject = new ArduinoBoard();
        }

        public void Init()
        {
            try
            {
                HWControllerObject.Open(FlowMain.Instance.DataMainObject.Data.HWController_PortName);
            }
            catch(Exception)
            {

            }
        }

        public void Close()
        {
            HWControllerObject.Close();
        }
    }
}
