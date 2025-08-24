using HealthMonitor.Core;
using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Systolic blood pressure vital sign checker.
    /// </summary>
    public class SystolicBloodPressure : IVitalSign {
        public string Name => "Systolic Blood Pressure";
        public string Unit => "mmHg";

        public bool IsWithinRange(float value, PatientProfile profile = null) {
            var (min, max) = GetRange(profile?.Age);
            return value >= min && value <= max;
        }

        private static (float min, float max) GetRange(int? age) {
            return age switch {
                < 12 => (80f, 120f),    // Child
                >= 65 => (90f, 150f),   // Elderly
                _ => (90f, 140f)        // Adult
            };
        }
    }
}