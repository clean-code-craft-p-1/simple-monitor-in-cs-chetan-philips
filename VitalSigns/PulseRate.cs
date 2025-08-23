using HealthMonitor.Core;
using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Implementation of pulse rate vital sign monitoring
    /// </summary>
    public class PulseRate : IVitalSign {
        public string Name => "Pulse Rate";
        public string Unit => "BPM";
        public string VendorInfo => "Digital Pulse Oximeter";

        /// <summary>Checks if pulse rate is within normal range (60-100 bpm)</summary>
        /// <param name="value">The pulse rate value in beats per minute</param>
        /// <returns>True if pulse rate is within normal range</returns>
        public bool IsWithinRange(float value) {
            return value >= 60 && value <= 100;
        }

        /// <summary>Checks if pulse rate is within range based on patient profile</summary>
        /// <param name="value">The pulse rate value in beats per minute</param>
        /// <param name="profile">The patient's profile</param>
        /// <returns>True if pulse rate is within patient-specific range</returns>
        public bool IsWithinRange(float value, PatientProfile profile) {
            if (profile?.Age < 18) {
                return value >= 70 && value <= 120;
            }
            if (profile?.Age > 65) {
                return value >= 50 && value <= 90;
            }
            return IsWithinRange(value);
        }
    }
}
