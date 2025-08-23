using HealthMonitor.Core;
using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Implementation of oxygen saturation vital sign monitoring
    /// </summary>
    public class OxygenSaturation : IVitalSign {
        public string Name => "Oxygen Saturation";
        public string Unit => "%";
        public string VendorInfo => "Pulse Oximeter SpO2 Sensor";

        /// <summary>Checks if SPO2 is within normal range (90-100%)</summary>
        /// <param name="value">The oxygen saturation percentage</param>
        /// <returns>True if SPO2 is within normal range</returns>
        public bool IsWithinRange(float value) {
            return value >= 90 && value <= 100;
        }

        /// <summary>Checks if SPO2 is within range based on patient profile</summary>
        /// <param name="value">The oxygen saturation percentage</param>
        /// <param name="profile">The patient's profile</param>
        /// <returns>True if SPO2 is within patient-specific range</returns>
        public bool IsWithinRange(float value, PatientProfile profile) {
            if (profile?.Age > 65) {
                return value >= 88 && value <= 100;
            }
            return IsWithinRange(value);
        }
    }
}
