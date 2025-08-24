using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns
{
    /// <summary>
    /// Base class for condition-based vital sign checkers to eliminate code duplication.
    /// </summary>
    public abstract class ConditionBasedVitalSign : BaseVitalSign 
    {
        // Protected constructor to prevent instantiation except by derived classes
        protected ConditionBasedVitalSign() : base() { }

        public override bool IsWithinRange(float value, PatientProfile profile = null) 
        {
            var (min, max) = GetConditionSpecificRange(profile);
            return value >= min && value <= max;
        }

        protected abstract (float min, float max) GetConditionSpecificRange(PatientProfile profile);
    }
}
