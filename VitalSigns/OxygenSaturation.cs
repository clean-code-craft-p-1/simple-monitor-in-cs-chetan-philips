using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Oxygen saturation vital sign checker with condition-based adjustments.
    /// </summary>
    public class OxygenSaturation : ConditionBasedVitalSign {
        public override string Name => "Oxygen Saturation";
        public override string Unit => "%";

        protected override (float min, float max) GetConditionSpecificRange(PatientProfile profile) {
            if (HasCOPD(profile)) {
                return (VitalRangeConstants.OXY_COPD, VitalRangeConstants.OXY_MAX);  // COPD patients have lower acceptable minimum
            }
            return (VitalRangeConstants.OXY_MIN, VitalRangeConstants.OXY_MAX);      // Normal range
        }

        private static bool HasCOPD(PatientProfile profile) {
            return profile?.MedicalConditions?.Contains("COPD") == true;
        }
    }
}