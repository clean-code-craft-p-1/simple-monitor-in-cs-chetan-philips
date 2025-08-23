using HealthMonitor.Core;
using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Implementation of temperature vital sign checking.
    /// Supports age-based range adjustments for different patient profiles.
    /// </summary>
    public class Temperature : IVitalSign {
        private const float BASE_MIN_TEMP = 95.0f;
        private const float BASE_MAX_TEMP = 102.0f;
        private const float CHILD_TEMP_ADJUSTMENT = 1.0f;
        private const float ELDERLY_TEMP_ADJUSTMENT = -1.0f;

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
            var (minTemp, maxTemp) = GetTemperatureRange(profile);
            return value >= minTemp && value <= maxTemp;
        }

        private (float min, float max) GetTemperatureRange(PatientProfile profile) {
            if (profile?.Age == null) {
                return GetAdultTemperatureRange();
            }

            return GetAgeSpecificTemperatureRange(profile.Age.Value);
        }

        private (float min, float max) GetAgeSpecificTemperatureRange(int age) {
            // Children may have slightly higher normal temperatures
            if (AgeClassifier.IsChild(age)) {
                return GetChildTemperatureRange();
            }

            // Elderly patients (65+) may have slightly lower normal temperatures
            if (AgeClassifier.IsElderly(age)) {
                return GetElderlyTemperatureRange();
            }

            return GetAdultTemperatureRange();
        }

        private static (float min, float max) GetAdultTemperatureRange() {
            return (BASE_MIN_TEMP, BASE_MAX_TEMP);
        }

        private static (float min, float max) GetChildTemperatureRange() {
            return (BASE_MIN_TEMP, BASE_MAX_TEMP + CHILD_TEMP_ADJUSTMENT);
        }

        private static (float min, float max) GetElderlyTemperatureRange() {
            return (BASE_MIN_TEMP + ELDERLY_TEMP_ADJUSTMENT, BASE_MAX_TEMP);
        }
    }
}