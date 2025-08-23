using HealthMonitor.Core;
using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Blood pressure vital sign checker with age-based adjustments.
    /// </summary>
    public class BloodPressure : IVitalSign {
        public string Name => "Blood Pressure";
        public string Unit => "mmHg";

        public bool IsWithinRange(float value, PatientProfile profile = null) {
            var (sysMin, sysMax, diaMin, diaMax) = GetRange(profile?.Age);
            return (value >= sysMin && value <= sysMax) || (value >= diaMin && value <= diaMax);
        }

        public bool IsWithinRange(float systolic, float diastolic, PatientProfile profile = null) {
            var (sysMin, sysMax, diaMin, diaMax) = GetRange(profile?.Age);
            return systolic >= sysMin && systolic <= sysMax && 
                   diastolic >= diaMin && diastolic <= diaMax;
        }

        private static (float sysMin, float sysMax, float diaMin, float diaMax) GetRange(int? age) {
            return age switch {
                < 12 => (80f, 120f, 50f, 80f),     // Child
                >= 65 => (100f, 150f, 65f, 95f),   // Elderly
                _ => (90f, 140f, 60f, 90f)          // Adult
            };
        }
    }
}
