using HealthMonitor.Infrastructure;

namespace HealthMonitor.Tests {
    /// <summary>
    /// Test implementation of IVitalSignAlerter for unit testing purposes.
    /// Test implementation of alerter that doesn't do I/O operations
    /// </summary>
    public class TestAlerter : IVitalSignAlerter {
        /// <summary>
        /// Alert count to track how many times the alert method has been called
        /// </summary>
        public int AlertCount { get; private set; }

        /// <summary>
        /// Alert method that increments the alert count
        /// </summary>
        /// <param name="message">The message to alert</param>
        public void Alert(string message) {
            AlertCount++;
        }
    }
}
