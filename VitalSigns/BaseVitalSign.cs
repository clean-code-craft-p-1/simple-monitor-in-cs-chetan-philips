using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Common base class for all vital sign implementations to eliminate duplication.
    /// </summary>
    public abstract class BaseVitalSign : IVitalSign {
        // Protected constructor to prevent instantiation except by derived classes
        protected BaseVitalSign() { }

        public abstract string Name { get; }
        public abstract string Unit { get; }

        public bool IsWithinRange(float value, PatientProfile profile = null) {
            var (min, max) = GetRange(value, profile);
            return value >= min && value <= max;
        }

        protected abstract (float min, float max) GetRange(float value, PatientProfile profile);
    }
}
