using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Systolic blood pressure vital sign checker.
    /// </summary>
    public class SystolicBloodPressure : AgeBasedVitalSign {
        public override string Name => "Systolic Blood Pressure";
        public override string Unit => "mmHg";

        protected override (float min, float max) GetAgeSpecificRange(int? age) {
            return age switch {
                < 12 => (80f, 120f),    // Child
                >= 65 => (90f, 150f),   // Elderly
                _ => (90f, 140f)        // Adult
            };
        }
    }
}