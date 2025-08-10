using HealthMonitor.Core;

namespace HealthMonitor {
    /// <summary>
    /// Main class for checking vitals, maintains backward compatibility with original interface
    /// </summary>
    public static class Checker {
        /// <summary>
        /// Checks if all vital signs are within normal range
        /// </summary>
        /// <param name="temperature">Temperature in Fahrenheit</param>
        /// <param name="pulseRate">Pulse rate in beats per minute</param>
        /// <param name="spo2">Oxygen saturation percentage</param>
        /// <returns>True if all vitals are within normal range</returns>
        public static bool VitalsOk(float temperature, int pulseRate, int spo2) {
            var vitalsChecker = new VitalsChecker();
            return vitalsChecker.CheckVitals(temperature, pulseRate, spo2);
        }
    }
}