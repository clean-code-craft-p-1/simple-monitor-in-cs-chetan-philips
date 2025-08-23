using HealthMonitor.Core;
using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Pulse rate vital sign checker with age-based adjustments.
    /// </summary>
    public class PulseRate : IVitalSign {
        public string Name => "Pulse Rate";
        public string Unit => "BPM";

        public bool IsWithinRange(float value, PatientProfile profile = null) {
            var (min, max) = GetRange(profile?.Age);
            return value >= min && value <= max;
        }

        private static (float min, float max) GetRange(int? age) {
            return age switch {
                < 12 => (70f, 120f),    // Child (higher range)
                >= 65 => (55f, 105f),   // Elderly (wider tolerance)
                _ => (60f, 100f)        // Adult
            };
        }
    }
}