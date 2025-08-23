using System;

namespace HealthMonitor.Infrastructure {
    /// <summary>
    /// Alerter implementation that writes alerts to the console
    /// </summary>
    public class VitalsAlerter : IVitalSignAlerter {
        /// <summary>
        /// Alert method that writes the alert message to the console
        /// </summary>
        /// <param name="message">The message to alert</param>
        public void Alert(string message) {
            Console.WriteLine(message);
        }
    }
}
