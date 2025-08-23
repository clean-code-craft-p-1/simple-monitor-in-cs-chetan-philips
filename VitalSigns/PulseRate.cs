using HealthMonitor.Core;
using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Implementation of pulse rate vital sign checking.
    /// Supports age-based range adjustments for different patient profiles.
    /// </summary>
    public class PulseRate : IVitalSign {
        private const float ADULT_MIN_PULSE = 60.0f;
        private const float ADULT_MAX_PULSE = 100.0f;
        private const float CHILD_MIN_PULSE = 70.0f;
        private const float CHILD_MAX_PULSE = 120.0f;
        private const float ELDERLY_MIN_PULSE = 55.0f;
        private const float ELDERLY_MAX_PULSE = 105.0f;

        /// <summary>
        /// Gets the name of this vital sign.
        /// </summary>
        public string Name => "Pulse Rate";

        /// <summary>
        /// Gets the unit of measurement for pulse rate.
        /// </summary>
        public string Unit => "BPM";

        /// <summary>
        /// Determines if the pulse rate is within normal range.
        /// Normal range: 60-100 BPM for adults, adjusted for age groups.
        /// </summary>
        /// <param name="value">Pulse rate value in beats per minute</param>
        /// <param name="profile">Patient profile for age-based adjustments</param>
        /// <returns>True if pulse rate is within normal range</returns>
        public bool IsWithinRange(float value, PatientProfile profile = null) {
            var (minPulse, maxPulse) = GetPulseRateRange(profile);
            return value >= minPulse && value <= maxPulse;
        }

        private (float min, float max) GetPulseRateRange(PatientProfile profile) {
            if (profile?.Age == null) {
                return GetAdultPulseRange();
            }

            return GetAgeSpecificPulseRange(profile.Age.Value);
        }

        private (float min, float max) GetAgeSpecificPulseRange(int age) {
            // Children typically have higher normal pulse rates
            if (AgeClassifier.IsChild(age)) {
                return GetChildPulseRange();
            }

            // Elderly may have slightly different ranges
            if (AgeClassifier.IsElderly(age)) {
                return GetElderlyPulseRange();
            }

            return GetAdultPulseRange();
        }

        private static (float min, float max) GetAdultPulseRange() {
            return (ADULT_MIN_PULSE, ADULT_MAX_PULSE);
        }

        private static (float min, float max) GetChildPulseRange() {
            return (CHILD_MIN_PULSE, CHILD_MAX_PULSE);
        }

        private static (float min, float max) GetElderlyPulseRange() {
            return (ELDERLY_MIN_PULSE, ELDERLY_MAX_PULSE);
        }
    }
}