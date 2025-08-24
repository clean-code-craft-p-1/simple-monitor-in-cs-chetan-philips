using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Pulse rate vital sign checker with age-based adjustments.
    /// </summary>
    public class PulseRate : AgeBasedVitalSign {
        public override string Name => "Pulse Rate";
        public override string Unit => "BPM";

        protected override (float min, float max) GetAgeSpecificRange(int? age) {
            return age switch {
                < 12 => (70f, 120f),    // Child (higher range)
                >= 65 => (55f, 105f),   // Elderly (wider tolerance)
                _ => (60f, 100f)        // Adult
            };
        }
    }
}