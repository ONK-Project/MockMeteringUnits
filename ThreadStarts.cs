using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MockMeteringsUnits
{
    public class ThreadStarts
    {
        const int generalFrequency = 900_000; // 15 minutes 
        const int waterFrequency = generalFrequency;
        const int heatFrequency = 4*generalFrequency;
        const int electricityFrequency = 8 * generalFrequency;
        
        public void HeatSubmission()
        {
            Thread.Sleep(heatFrequency);
        }

        public void WaterSubmission()
        {
            Thread.Sleep(waterFrequency);
        }

        public void ElectricitySubmission()
        {
            Thread.Sleep(electricityFrequency);
        }
    }
}
