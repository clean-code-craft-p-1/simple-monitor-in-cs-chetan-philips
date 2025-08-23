using HealthMonitor.Core;
using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Temperature vital sign checker with age-based adjustments.
    /// </summary>
    public class Temperature : IVitalSign {
        public string Name => "Temperature";
        public string Unit => "°F";

        public bool IsWithinRange(float value, PatientProfile profile = null) {
            var (min, max) = GetRange(profile?.Age);
            return value >= min && value <= max;
        }

        private static (float min, float max) GetRange(int? age) {
            return age switch {
                < 12 => (95f, 103f),    // Child (higher tolerance)
                >= 65 => (94f, 102f),   // Elderly (lower minimum)
                _ => (95f, 102f)        // Adult
            };
        }
    }
}