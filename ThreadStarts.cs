using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MockMeteringsUnits
{
    public class ThreadStarts
    {
        // For delay/frequency
        const int generalFrequency = 900_000; // 15 minutes 
        const int waterFrequency = generalFrequency;
        const int heatFrequency = 4*generalFrequency;
        const int electricityFrequency = 8 * generalFrequency;

        // For random meassurement
        Random random = new Random();
        const double minimum = 0.0;
        const double maximum = 100.0;

        List<long> heatMeteringUnitIds;
        List<long> waterMeteringUnitIds;
        List<long> electricityMeteringUnitIds;

        public ThreadStarts()
        {
            //TODO: Retrieve all sensors

            //TODO: Fill the 3 lists dependent of the type of sensor retrieved

            // Start 3 threads
            var heatThread = new Thread(new ThreadStart(HeatSubmission));
            heatThread.Start();

            var waterThread = new Thread(new ThreadStart(WaterSubmission));
            waterThread.Start();

            var electrictyThread = new Thread(new ThreadStart(ElectricitySubmission));
            electrictyThread.Start();
        }

        public void HeatSubmission()
        {
            foreach (var heatId in heatMeteringUnitIds)
            {
                var submission = new Models.Submission()
                {
                    MeteringUnit = new Models.MeteringUnit()
                    {
                        id = heatId,
                        model = "AAA-BBB-CCC",
                        type = "Heat",
                        location = $"8000, Aarhus C, Fiskergade {(heatId % 5).ToString()};"
                    },
                    DateTime = DateTime.UtcNow,
                    RessourceUsage = (random.NextDouble() * (maximum - minimum) + minimum),
                    UnitOfMeassure = "kWh/m^3"
                };

                //TODO: Call Ingress :) 
            }
            Thread.Sleep(heatFrequency);
            HeatSubmission();
        }

        public void WaterSubmission()
        {
            foreach (var waterId in waterMeteringUnitIds)
            {
                var submission = new Models.Submission()
                {
                    MeteringUnit = new Models.MeteringUnit()
                    {
                        id = waterId,
                        model = "CCC-BBB-AAA",
                        type = "Water",
                        location = $"8000, Aarhus C, Fiskergade {(waterId % 5).ToString()};"
                    },
                    DateTime = DateTime.UtcNow,
                    RessourceUsage = (random.NextDouble() * (maximum - minimum) + minimum),
                    UnitOfMeassure = "kWh/m^3"
                };

                //TODO: Call Ingress :) 
            }
            Thread.Sleep(waterFrequency);
            WaterSubmission();
        }

        public void ElectricitySubmission()
        {
            foreach (var electricityID in electricityMeteringUnitIds)
            {
                var submission = new Models.Submission()
                {
                    MeteringUnit = new Models.MeteringUnit()
                    {
                        id = electricityID,
                        model = "BBB-AAA-CCC",
                        type = "Electricity",
                        location = $"8000, Aarhus C, Fiskergade {(electricityID % 5).ToString()};"
                    },
                    DateTime = DateTime.UtcNow,
                    RessourceUsage = (random.NextDouble() * (maximum - minimum) + minimum),
                    UnitOfMeassure = "kWh/m^3"
                };

                //TODO: Call Ingress :) 
            }
            Thread.Sleep(electricityFrequency);
            ElectricitySubmission();
        }
    }
}
