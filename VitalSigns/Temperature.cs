using HealthMonitor.Core;
using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Implementation of temperature vital sign checking.
    /// Supports age-based range adjustments for different patient profiles.
    /// </summary>
    public class Temperature : IVitalSign {
        /// <summary>
        /// Gets the name of this vital sign.
        /// </summary>
        public string Name => "Temperature";

        /// <summary>
        /// Gets the unit of measurement for temperature.
        /// </summary>
        public string Unit => "°F";

        /// <summary>
        /// Determines if the temperature is within normal range.
        /// Normal range: 95-102°F for adults, adjusted for age groups.
        /// </summary>
        /// <param name="value">Temperature value in Fahrenheit</param>
        /// <param name="profile">Patient profile for age-based adjustments</param>
        /// <returns>True if temperature is within normal range</returns>
        public bool IsWithinRange(float value, PatientProfile profile = null) {
            const float baseMinTemp = 95.0f;
            const float baseMaxTemp = 102.0f;

            if (profile?.Age != null) {
                // Elderly patients (65+) may have slightly lower normal temperatures
                if (profile.Age >= 65) {
                    return value >= (baseMinTemp - 1.0f) && value <= baseMaxTemp;
                }
                // Children may have slightly higher normal temperatures
                if (profile.Age < 12) {
                    return value >= baseMinTemp && value <= (baseMaxTemp + 1.0f);
                }
            }

            return value >= baseMinTemp && value <= baseMaxTemp;
        }
    }
}