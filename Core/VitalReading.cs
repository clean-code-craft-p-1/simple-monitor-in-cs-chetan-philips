using System.Collections.Generic;

namespace HealthMonitor.Core {
    /// <summary>
    /// Represents vital sign readings with extensible support.
    /// </summary>
    public class VitalReading {
        private readonly Dictionary<string, float> _readings = new();

        public VitalReading() { }

        // Convenient constructors
        public VitalReading(float temperature, float pulseRate, float oxygenSaturation) {
            SetReading("Temperature", temperature);
            SetReading("Pulse Rate", pulseRate);
            SetReading("Oxygen Saturation", oxygenSaturation);
        }

        public VitalReading(float temperature, float pulseRate, float oxygenSaturation,
                          float systolicBP, float diastolicBP) {
            SetReading("Temperature", temperature);
            SetReading("Pulse Rate", pulseRate);
            SetReading("Oxygen Saturation", oxygenSaturation);
            SetReading("Systolic Blood Pressure", systolicBP);
            SetReading("Diastolic Blood Pressure", diastolicBP);
        }

        public void SetReading(string vitalName, float value) {
            if (value > 0) _readings[vitalName] = value;
        }

        public float GetReading(string vitalName) {
            return _readings.TryGetValue(vitalName, out float value) ? value : 0f;
        }

        public bool HasReading(string vitalName) {
            return _readings.ContainsKey(vitalName);
        }

        public IEnumerable<string> GetVitalNames() {
            return _readings.Keys;
        }
    }
}