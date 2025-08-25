using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Configurable vital sign implementation that supports both age-based and condition-based ranges.
    /// Replaces the deep inheritance hierarchy with a composition-based approach.
    /// </summary>
    public class ConfigurableVitalSign : IVitalSign {
        private readonly VitalRangeConfig _config;

        /// <summary>
        /// Creates a new configurable vital sign with the specified configuration
        /// </summary>
        /// <param name="config">The configuration for this vital sign</param>
        public ConfigurableVitalSign(VitalRangeConfig config) {
            _config = config;
        }

        /// <summary>
        /// Gets the name of the vital sign
        /// </summary>
        public string Name => _config.Name;

        /// <summary>
        /// Gets the unit of measurement for the vital sign
        /// </summary>
        public string Unit => _config.Unit;

        /// <summary>
        /// Determines whether a given value is within the acceptable range for this vital sign
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <param name="profile">Optional patient profile for personalized ranges</param>
        /// <returns>True if the value is within range, otherwise false</returns>
        public bool IsWithinRange(float value, PatientProfile profile = null) {
            var (min, max) = _config.GetRange(profile);
            return value >= min && value <= max;
        }
    }
}
