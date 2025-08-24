using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Temperature vital sign checker with age-based adjustments.
    /// </summary>
    public class Temperature : AgeBasedVitalSign {
        public override string Name => "Temperature";
        public override string Unit => "°F";

        protected override (float min, float max) GetAgeSpecificRange(int? age) {
            return age switch {
                < 12 => (95f, 103f),    // Child (higher tolerance)
                >= 65 => (94f, 102f),   // Elderly (lower minimum)
                _ => (95f, 102f)        // Adult
            };
        }
    }
}