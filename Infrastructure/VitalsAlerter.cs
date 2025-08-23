using System;

namespace HealthMonitor.Infrastructure {
    /// <summary>
    /// Console-based implementation of the vital sign alerting system.
    /// Outputs alert messages to the console for monitoring personnel.
    /// </summary>
    public class VitalsAlerter : IVitalSignAlerter {
        /// <summary>
        /// Sends an alert message to the console for an out-of-range vital sign.
        /// </summary>
        /// <param name="vitalName">Name of the vital sign that is out of range</param>
        /// <param name="value">The actual value that triggered the alert</param>
        /// <param name="unit">Unit of measurement for the vital sign</param>
        public void Alert(string vitalName, float value, string unit) {
            Console.WriteLine($"?? ALERT: {vitalName} is {value} {unit} - Outside normal range!");
        }
    }
}