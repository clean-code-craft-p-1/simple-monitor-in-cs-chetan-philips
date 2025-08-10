using System;
using System.Collections.Generic;

namespace HealthMonitor
{
    /// <summary>
    /// Class responsible for checking vital signs
    /// </summary>
    public class VitalsChecker
    {
        private readonly VitalsAlerter _alerter;
        private readonly IVitalSign _temperatureVital;
        private readonly IVitalSign _pulseRateVital;
        private readonly IVitalSign _spo2Vital;

        /// <summary>
        /// Default constructor that initializes with standard vital sign checkers
        /// </summary>
        public VitalsChecker()
            : this(new VitalsAlerter(), new Temperature(), new PulseRate(), new OxygenSaturation())
        {
        }

        /// <summary>
        /// Constructor with dependency injection for better testability
        /// </summary>
        /// <param name="alerter">The alerter to use for out-of-range notifications</param>
        /// <param name="temperatureVital">The temperature checker</param>
        /// <param name="pulseRateVital">The pulse rate checker</param>
        /// <param name="spo2Vital">The oxygen saturation checker</param>
        public VitalsChecker(
            VitalsAlerter alerter,
            IVitalSign temperatureVital,
            IVitalSign pulseRateVital,
            IVitalSign spo2Vital)
        {
            _alerter = alerter;
            _temperatureVital = temperatureVital;
            _pulseRateVital = pulseRateVital;
            _spo2Vital = spo2Vital;
        }

        /// <summary>
        /// Main method for checking vitals with I/O operations
        /// </summary>
        /// <param name="temperature">Temperature in Fahrenheit</param>
        /// <param name="pulseRate">Pulse rate in beats per minute</param>
        /// <param name="spo2">Oxygen saturation percentage</param>
        /// <returns>True if all vitals are within normal range</returns>
        public virtual bool CheckVitals(float temperature, float pulseRate, float spo2)
        {
            var readings = new List<VitalReading>
            {
                new VitalReading(_temperatureVital, temperature),
                new VitalReading(_pulseRateVital, pulseRate),
                new VitalReading(_spo2Vital, spo2)
            };

            var allVitalsOk = AreAllVitalsWithinRange(readings);

            // Handling the I/O operations separately from the checking logic
            if (!allVitalsOk)
            {
                // Alert for the first out-of-range vital
                var outOfRangeReading = readings.Find(r => !r.IsWithinRange);
                _alerter.Alert(outOfRangeReading);
            }
            else
            {
                Console.WriteLine("Vitals received within normal range");
                Console.WriteLine($"Temperature: {temperature} Pulse: {pulseRate}, SO2: {spo2}");
            }

            return allVitalsOk;
        }

        /// <summary>
        /// Pure function for checking if all vitals are within range (no I/O)
        /// </summary>
        /// <param name="readings">The list of vital readings to check</param>
        /// <returns>True if all readings are within their normal ranges</returns>
        public bool AreAllVitalsWithinRange(List<VitalReading> readings)
        {
            foreach (var reading in readings)
            {
                if (!reading.IsWithinRange)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
