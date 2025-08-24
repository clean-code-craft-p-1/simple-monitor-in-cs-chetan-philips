using HealthMonitor.Core;

namespace HealthMonitor.Tests {
    /// <summary>
    /// Test implementation of IVitalSignAlerter for testing purposes.
    /// Tracks alert calls instead of outputting to console.
    /// </summary>
    public class VitalSignAlerterTests : IVitalSignAlerter {
        /// <summary>
        /// Gets the number of alerts that have been triggered.
        /// </summary>
        public int AlertCount { get; private set; }

        /// <summary>
        /// Gets the last vital sign name that triggered an alert.
        /// </summary>
        public string LastVitalName { get; private set; }

        /// <summary>
        /// Gets the last value that triggered an alert.
        /// </summary>
        public string LastValue { get; private set; }

        /// <summary>
        /// Gets the last unit of measurement that triggered an alert.
        /// </summary>
        public string LastUnit { get; private set; }

        /// <summary>
        /// Captures alert information for testing without performing I/O.
        /// </summary>
        /// <param name="vitalName">Name of the vital sign that triggered the alert</param>
        /// <param name="value">The out-of-range value</param>
        /// <param name="unit">Unit of measurement</param>
        public void Alert(string vitalName, string value, string unit) {
            AlertCount++;
            LastVitalName = vitalName;
            LastValue = value;
            LastUnit = unit;
        }

        /// <summary>
        /// Resets the alert counter and message for new test scenarios.
        /// </summary>
        public void Reset() {
            AlertCount = 0;
            LastVitalName = null;
            LastValue = null;
            LastUnit = null;
        }
    }
}