using HealthMonitor.Core;
using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Implementation of oxygen saturation vital sign checking.
    /// Supports patient-specific range adjustments.
    /// </summary>
    public class OxygenSaturation : IVitalSign {
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
            const float minOxygenSat = 90.0f;
            const float maxOxygenSat = 100.0f;

            // Some medical conditions may require adjusted ranges
            if (profile?.MedicalConditions != null &&
                profile.MedicalConditions.Contains("COPD")) {
                // COPD patients may have lower acceptable ranges
                return value >= 85.0f && value <= maxOxygenSat;
            }

            return value >= minOxygenSat && value <= maxOxygenSat;
        }
    }
}