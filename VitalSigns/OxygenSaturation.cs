using HealthMonitor.Core;
using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Implementation of oxygen saturation vital sign checking.
    /// Supports patient-specific range adjustments.
    /// </summary>
    public class OxygenSaturation : IVitalSign {
        private const float NORMAL_MIN_OXYGEN = 90.0f;
        private const float NORMAL_MAX_OXYGEN = 100.0f;
        private const float COPD_MIN_OXYGEN = 85.0f;
        private const string COPD_CONDITION = "COPD";

        /// <summary>
        /// Gets the name of this vital sign.
        /// </summary>
        public string Name => "Oxygen Saturation";

        /// <summary>
        /// Gets the unit of measurement for oxygen saturation.
        /// </summary>
        public string Unit => "%";

        /// <summary>
        /// Determines if the oxygen saturation is within normal range.
        /// Normal range: 90-100% for most patients.
        /// </summary>
        /// <param name="value">Oxygen saturation percentage</param>
        /// <param name="profile">Patient profile for condition-based adjustments</param>
        /// <returns>True if oxygen saturation is within normal range</returns>
        public bool IsWithinRange(float value, PatientProfile profile = null) {
            var (minOxygen, maxOxygen) = GetOxygenSaturationRange(profile);
            return value >= minOxygen && value <= maxOxygen;
        }

        private (float min, float max) GetOxygenSaturationRange(PatientProfile profile) {
            // Some medical conditions may require adjusted ranges
            if (HasCOPD(profile)) {
                // COPD patients may have lower acceptable ranges
                return (COPD_MIN_OXYGEN, NORMAL_MAX_OXYGEN);
            }

            return (NORMAL_MIN_OXYGEN, NORMAL_MAX_OXYGEN);
        }

        private static bool HasCOPD(PatientProfile profile) {
            return profile?.MedicalConditions?.Contains(COPD_CONDITION) == true;
        }
    }
}