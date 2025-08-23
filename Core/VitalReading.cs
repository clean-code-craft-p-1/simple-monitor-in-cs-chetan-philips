using System.Collections.Generic;

namespace HealthMonitor.Core {
    /// <summary>
    /// Represents vital sign readings using a flexible dictionary approach.
    /// Easily extensible for new vital signs without code changes.
    /// </summary>
    public class VitalReading {
        private readonly Dictionary<string, float> _readings = new Dictionary<string, float>();

        public VitalReading() { }

        // Convenient constructor for common vitals
        public VitalReading(float temperature, int pulseRate, float oxygenSaturation) {
            SetReading("Temperature", temperature);
            SetReading("Pulse Rate", pulseRate);
            SetReading("Oxygen Saturation", oxygenSaturation);
        }

        // Constructor with blood pressure
        public VitalReading(float temperature, int pulseRate, float oxygenSaturation,
                          float systolic, float diastolic) : this(temperature, pulseRate, oxygenSaturation) {
            SetReading("Systolic Blood Pressure", systolic);
            SetReading("Diastolic Blood Pressure", diastolic);
        }

        public void SetReading(string vitalName, float value) => _readings[vitalName] = value;

        public float GetReading(string vitalName) => _readings.TryGetValue(vitalName, out var value) ? value : 0f;

        public bool HasReading(string vitalName) => _readings.ContainsKey(vitalName);

        public IEnumerable<string> GetVitalNames() => _readings.Keys;
    }
}