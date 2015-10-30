using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSS_Host_Module.Data
{
    public class DataMain
    {
        public AppSettings Data { get; set; }

        public DataMain()
        {
            Data = new AppSettings();
            try
            {
                Data = Data.Load();
            }
            catch(Exception)
            {
                Data.Save();
            }
        }

        public void Init()
        {
            //load data from file
        }

        public void Close()
        {

        }

        public void SaveData()
        {
            Data.Save();
        }
    }
}
