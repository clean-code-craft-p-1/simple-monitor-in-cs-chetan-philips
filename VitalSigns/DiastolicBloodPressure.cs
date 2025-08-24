using HealthMonitor.Core;
using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Diastolic blood pressure vital sign checker.
    /// </summary>
    public class DiastolicBloodPressure : IVitalSign {
        public string Name => "Diastolic Blood Pressure";
        public string Unit => "mmHg";

        public bool IsWithinRange(float value, PatientProfile profile = null) {
            var (min, max) = GetRange(profile?.Age);
            return value >= min && value <= max;
        }

        private static (float min, float max) GetRange(int? age) {
            return age switch {
                < 12 => (50f, 80f),     // Child
                >= 65 => (65f, 95f),    // Elderly
                _ => (60f, 90f)         // Adult
            };
        }
    }
}