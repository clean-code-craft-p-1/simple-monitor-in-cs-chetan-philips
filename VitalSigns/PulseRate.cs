using HealthMonitor.Core;
using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Implementation of pulse rate vital sign monitoring
    /// </summary>
    public class PulseRate : IVitalSign {
        // Standard pulse rate ranges in beats per minute
        private const float StandardMinimum = 60.0f;
        private const float StandardMaximum = 100.0f;

        // Child-specific adjustments (children typically have higher pulse rates)
        private const float ChildMinimumAdjustment = 10.0f;
        private const float ChildMaximumAdjustment = 20.0f;

        // Elderly-specific adjustments (elderly may have lower limits)
        private const float ElderlyMinimumAdjustment = -5.0f;
        private const float ElderlyMaximumAdjustment = -10.0f;

        /// <summary>Gets the name of this vital sign</summary>
        public string Name => "Pulse Rate";

        /// <summary>Gets the unit of measurement</summary>
        public string Unit => "bpm";

        /// <summary>Gets vendor-specific information</summary>
        public string VendorInfo => "Standard Pulse Monitor";

        /// <summary>Checks if pulse rate is within normal range (60-100 bpm)</summary>
        /// <param name="value">The pulse rate value in beats per minute</param>
        /// <returns>True if pulse rate is within normal range</returns>
        public bool IsWithinRange(float value) => value >= StandardMinimum && value <= StandardMaximum;

        /// <summary>Checks if pulse rate is within range based on patient profile</summary>
        /// <param name="value">The pulse rate value in beats per minute</param>
        /// <param name="patientProfile">The patient's profile</param>
        /// <returns>True if pulse rate is within patient-specific range</returns>
        public bool IsWithinRange(float value, PatientProfile patientProfile) {
            var (min, max) = GetRange(patientProfile);
            return value >= min && value <= max;
        }

        /// <summary>Gets the normal pulse rate range</summary>
        /// <returns>Minimum and maximum normal pulse rate values</returns>
        public (float Min, float Max) GetRange() => (StandardMinimum, StandardMaximum);

        /// <summary>Gets patient-specific pulse rate range</summary>
        /// <param name="patientProfile">The patient's profile</param>
        /// <returns>Patient-specific minimum and maximum normal pulse rate values</returns>
        public (float Min, float Max) GetRange(PatientProfile patientProfile) {
            float minAdjustment = 0;
            float maxAdjustment = 0;

            if (patientProfile.IsChild) {
                minAdjustment += ChildMinimumAdjustment;
                maxAdjustment += ChildMaximumAdjustment;
            }

            if (patientProfile.IsElderly) {
                minAdjustment += ElderlyMinimumAdjustment;
                maxAdjustment += ElderlyMaximumAdjustment;
            }

            return (StandardMinimum + minAdjustment, StandardMaximum + maxAdjustment);
        }
    }
}
