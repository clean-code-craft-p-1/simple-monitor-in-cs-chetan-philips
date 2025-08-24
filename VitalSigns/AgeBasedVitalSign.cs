using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns
{
    /// <summary>
    /// Base class for age-based vital sign checkers to eliminate code duplication.
    /// </summary>
    public abstract class AgeBasedVitalSign : IVitalSign 
    {
        // Protected constructor to prevent instantiation except by derived classes
        protected AgeBasedVitalSign() { }
        public abstract string Name { get; }
        public abstract string Unit { get; }

        public bool IsWithinRange(float value, PatientProfile profile = null) {
            var (min, max) = GetAgeSpecificRange(profile?.Age);
            return value >= min && value <= max;
        }

        protected abstract (float min, float max) GetAgeSpecificRange(int? age);
    }
}