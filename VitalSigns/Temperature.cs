using HealthMonitor.Core;
using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Implementation of temperature vital sign monitoring
    /// </summary>
    public class Temperature : IVitalSign {
        public string Name => "Temperature";
        public string Unit => "°F";
        public string VendorInfo => "Standard Digital Thermometer";

        /// <summary>Checks if temperature is within normal range (95-102 °F)</summary>
        /// <param name="value">The temperature value in Fahrenheit</param>
        /// <returns>True if temperature is within normal range</returns>
        public bool IsWithinRange(float value) {
            return value >= 95.0f && value <= 102.0f;
        }

        /// <summary>Checks if temperature is within range based on patient profile</summary>
        /// <param name="value">The temperature value in Fahrenheit</param>
        /// <param name="profile">The patient's profile</param>
        /// <returns>True if temperature is within patient-specific range</returns>
        public bool IsWithinRange(float value, PatientProfile profile) {
            if (profile?.Age < 18) {
                return value >= 96.0f && value <= 101.0f;
            }
            return IsWithinRange(value);
        }
    }
}
