using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns
{
    /// <summary>
    /// Oxygen saturation vital sign checker with condition-based adjustments.
    /// </summary>
    public class OxygenSaturation : IVitalSign {
        public string Name => "Oxygen Saturation";
        public string Unit => "%";

        public bool IsWithinRange(float value, PatientProfile profile = null) {
            var (min, max) = GetRange(profile);
            return value >= min && value <= max;
        }

        private static (float min, float max) GetRange(PatientProfile profile) {
            if (HasCOPD(profile)) {
                return (85f, 100f);  // COPD patients have lower acceptable minimum
            }
            return (90f, 100f);      // Normal range
        }

        private static bool HasCOPD(PatientProfile profile) {
            return profile?.MedicalConditions?.Contains("COPD") == true;
        }
    }
}