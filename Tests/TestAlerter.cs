using HealthMonitor.Infrastructure;

namespace HealthMonitor.Tests {
    /// <summary>
    /// Test implementation of IVitalSignAlerter that captures alerts for testing.
    /// Does not perform actual I/O operations during unit tests.
    /// </summary>
    public class TestAlerter : IVitalSignAlerter {
        /// <summary>
        /// Gets the number of alerts that have been triggered.
        /// </summary>
        public int AlertCount { get; private set; }

        /// <summary>
        /// Gets the last alert message that was triggered.
        /// </summary>
        public string LastAlertMessage { get; private set; }

        /// <summary>
        /// Captures alert information for testing without performing I/O.
        /// </summary>
        /// <param name="vitalName">Name of the vital sign that triggered the alert</param>
        /// <param name="value">The out-of-range value</param>
        /// <param name="unit">Unit of measurement</param>
        public void Alert(string vitalName, float value, string unit) {
            AlertCount++;
            LastAlertMessage = $"{vitalName} is {value} {unit}";
        }

        /// <summary>
        /// Resets the alert counter and message for new test scenarios.
        /// </summary>
        public void Reset() {
            AlertCount = 0;
            LastAlertMessage = null;
        }
    }
}