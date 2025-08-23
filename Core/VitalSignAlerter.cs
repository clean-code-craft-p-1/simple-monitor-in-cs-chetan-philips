using System;

namespace HealthMonitor.Core {
    /// <summary>
    /// Simple console-based alerter.
    /// </summary>
    public class VitalSignAlerter : IVitalSignAlerter {
        public void Alert(string vitalName, string value, string unit) {
            Console.WriteLine($"ALERT: {vitalName} is {value} {unit}");
        }
    }
}