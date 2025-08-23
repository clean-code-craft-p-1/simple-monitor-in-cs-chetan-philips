using HealthMonitor.Core;

namespace HealthMonitor.Tests {
    /// <summary>
    /// Test implementation of IVitalSignAlerter for testing purposes.
    /// </summary>
    public class VitalSignAlerterTests : IVitalSignAlerter {
        public int AlertCount { get; private set; }
        public string LastVitalName { get; private set; }
        public string LastValue { get; private set; }
        public string LastUnit { get; private set; }

        public void Alert(string vitalName, string value, string unit) {
            AlertCount++;
            LastVitalName = vitalName;
            LastValue = value;
            LastUnit = unit;
        }

        public void Reset() {
            AlertCount = 0;
            LastVitalName = null;
            LastValue = null;
            LastUnit = null;
        }
    }
}