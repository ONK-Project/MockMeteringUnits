using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using Models;
using System.Linq;

namespace MockMeteringsUnits
{
    public class ThreadStarts
    {
        // For HTTP
        static readonly HttpClient client = new HttpClient();

        // For delay/frequency
        const int generalFrequency = 900_000; // 15 minutes 
        const int waterFrequency = generalFrequency;
        const int heatFrequency = 4*generalFrequency;
        const int electricityFrequency = 8 * generalFrequency;

        // For random meassurement
        Random random = new Random();
        const double minimum = 0.0;
        const double maximum = 100.0;

        List<MeteringUnit> heatMeteringUnits;
        List<MeteringUnit> waterMeteringUnits;
        List<MeteringUnit> electricityMeteringUnits;

        public ThreadStarts()
        {
            // Retrieve all sensors 
            var response = client.GetStringAsync("http://35.187.106.129:8080/api/v1/MeteringUnit").Result;
            var meteringUnits = JsonConvert.DeserializeObject<List<MeteringUnit>>(response);


           // Fill the 3 lists dependent of the type of sensor retrieved
            heatMeteringUnits = meteringUnits
                .Where(m => m.Type == "Heat")
                .ToList();

            waterMeteringUnits = meteringUnits
                .Where(m => m.Type == "Water")
                .ToList();

            electricityMeteringUnits = meteringUnits
                .Where(m => m.Type == "Electricity")
                .ToList();


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
            foreach (var heat in heatMeteringUnits)
            {
                var submission = new Submission()
                {
                    MeteringUnit = new MeteringUnit()
                    {
                        MeteringUnitId = heat.MeteringUnitId,
                        Model = heat.Model,
                        Type = heat.Type,
                        Location = heat.Location
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
            foreach (var water in waterMeteringUnits)
            {
                var submission = new Submission()
                {
                    MeteringUnit = new MeteringUnit()
                    {
                        MeteringUnitId = water.MeteringUnitId,
                        Model = water.Model,
                        Type = water.Type,
                        Location = water.Location
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
            foreach (var electricity in electricityMeteringUnits)
            {
                var submission = new Submission()
                {
                    MeteringUnit = new MeteringUnit()
                    {
                        MeteringUnitId = electricity.MeteringUnitId,
                        Model = electricity.Model,
                        Type = electricity.Type,
                        Location = electricity.Location
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
