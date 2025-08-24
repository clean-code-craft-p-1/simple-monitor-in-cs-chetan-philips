using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Diastolic blood pressure vital sign checker.
    /// </summary>
    public class DiastolicBloodPressure : AgeBasedVitalSign {
        public override string Name => "Diastolic Blood Pressure";
        public override string Unit => "mmHg";

        protected override (float min, float max) GetAgeSpecificRange(int? age) {
            return age switch {
                < 12 => (50f, 80f),     // Child
                >= 65 => (65f, 95f),    // Elderly
                _ => (60f, 90f)         // Adult
            };
        }
    }
}