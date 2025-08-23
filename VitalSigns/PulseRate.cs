using HealthMonitor.Core;
using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Implementation of pulse rate vital sign checking.
    /// Supports age-based range adjustments for different patient profiles.
    /// </summary>
    public class PulseRate : IVitalSign {
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
            const float baseMinPulse = 60.0f;
            const float baseMaxPulse = 100.0f;

            if (profile?.Age != null) {
                // Children typically have higher normal pulse rates
                if (profile.Age < 12) {
                    return value >= 70.0f && value <= 120.0f;
                }
                // Elderly may have slightly different ranges
                if (profile.Age >= 65) {
                    return value >= 55.0f && value <= 105.0f;
                }
            }

            return value >= baseMinPulse && value <= baseMaxPulse;
        }
    }
}