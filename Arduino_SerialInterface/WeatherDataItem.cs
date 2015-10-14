using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arduino_SerialInterface
{
    class WeatherDataItem
    {
        DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        float temparatureCelsius;

        public float TemparatureCelsius
        {
            get { return temparatureCelsius; }
            set { temparatureCelsius = value; }
        }

        public void FromString(string weatherDataItemString)
        {
            string[] weatherDataItemArray = weatherDataItemString.Split('=');
            DateTime.TryParse(weatherDataItemArray[0], out date);
            float.TryParse(weatherDataItemArray[1].Replace('.', ','), out temparatureCelsius);
        }
    }
}
