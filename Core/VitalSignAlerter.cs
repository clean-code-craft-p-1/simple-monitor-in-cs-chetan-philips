using System;

namespace HealthMonitor.Core {
    /// <summary>
    /// Console-based implementation of vital sign alerting.
    /// </summary>
    public class VitalSignAlerter : IVitalSignAlerter {
        public void Alert(string vitalName, string value, string unit) {
            Console.WriteLine($"ALERT: {vitalName} is {value} {unit}");
        }
    }
}