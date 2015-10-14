using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSS_Host_Module
{
    public class Singleton<T> where T : new()
    {
        static T _instance = default(T);

        protected Singleton()
        {
        }

        static public T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }
    }
}
