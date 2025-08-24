using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Base class for age-based vital sign checkers to eliminate code duplication.
    /// </summary>
    public abstract class AgeBasedVitalSign : BaseVitalSign {
        // Default constructor
        protected AgeBasedVitalSign() { }

        protected override (float min, float max) GetRange(float value, PatientProfile profile) {
            return GetAgeSpecificRange(profile?.Age);
        }

        protected abstract (float min, float max) GetAgeSpecificRange(int? age);
    }
}