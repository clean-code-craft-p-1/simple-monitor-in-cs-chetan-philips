using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Base class for condition-based vital sign checkers to eliminate code duplication.
    /// </summary>
    public abstract class ConditionBasedVitalSign : BaseVitalSign {
        // Protected constructor to prevent instantiation except by derived classes
        protected ConditionBasedVitalSign() : base() { }

        protected override (float min, float max) GetRange(float value, PatientProfile profile) {
            return GetConditionSpecificRange(profile);
        }

        protected abstract (float min, float max) GetConditionSpecificRange(PatientProfile profile);
    }
}
