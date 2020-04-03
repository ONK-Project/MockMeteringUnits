using System;
using System.Collections.Generic;
using System.Threading;

namespace MockMeteringsUnits
{
    class Program
    {
        static void Main(string[] args)
        {
            // Retrieve all sensors

            // Make 3 lists of metering unit guids 

            List<long> heatMeteringUnitIds;
            List<long> waterMeteringUnitIds;
            List<long> electricityMeteringUnitIds;

            // Fill the 3 lists dependent of the type of sensor retrieved

            // Start 3 threads with different delay doing different submissions
            var starts = new ThreadStarts();

            var heatThread = new Thread(new ThreadStart(starts.HeatSubmission));
            heatThread.Start();

            var waterThread = new Thread(new ThreadStart(starts.WaterSubmission));
            waterThread.Start();

            var electrictyThread = new Thread(new ThreadStart(starts.ElectricitySubmission));
            electrictyThread.Start();
        }
    }
}
