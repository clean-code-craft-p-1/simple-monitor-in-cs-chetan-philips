using HealthMonitor.Infrastructure;
using HealthMonitor.Models;

namespace HealthMonitor.Core {
    /// <summary>
    /// Class responsible for checking vital signs
    /// </summary>
    public class VitalsChecker {
        private readonly IVitalSignAlerter _alerter;

        /// <summary>
        /// Constructor that accepts an IVitalSignAlerter implementation
        /// </summary>
        public VitalsChecker(IVitalSignAlerter alerter) {
            _alerter = alerter ?? new VitalsAlerter();
        }

        /// <summary>
        /// Checks if all vital signs are within the normal range.
        /// </summary>
        /// <param name="vitals">Vital readings to check</param>
        /// <param name="profile">Optional patient profile for personalized ranges</param>
        /// <returns></returns>
        public bool AreAllVitalsWithinRange(VitalReading vitals, PatientProfile profile = null) {
            return IsTemperatureWithinRange(vitals.Temperature, profile) &&
                   IsPulseRateWithinRange(vitals.PulseRate, profile) &&
                   IsOxygenSaturationWithinRange(vitals.OxygenSaturation, profile);
        }

        /// <summary>
        /// Checks individual vital signs and alerts if any are critical.
        /// </summary>
        /// <param name="vitals">Vital readings to check</param>
        /// <param name="profile">Optional patient profile for personalized ranges</param>
        public void CheckVitals(VitalReading vitals, PatientProfile profile = null) {
            CheckTemperature(vitals.Temperature, profile);
            CheckPulseRate(vitals.PulseRate, profile);
            CheckOxygenSaturation(vitals.OxygenSaturation, profile);
        }

        private bool IsTemperatureWithinRange(float temperature, PatientProfile profile) {
            var tempVital = VitalSignFactory.Create(VitalSignType.Temperature);
            return profile == null ?
                tempVital.IsWithinRange(temperature) :
                tempVital.IsWithinRange(temperature, profile);
        }

        private bool IsPulseRateWithinRange(int pulseRate, PatientProfile profile) {
            var pulseVital = VitalSignFactory.Create(VitalSignType.PulseRate);
            return profile == null ?
                pulseVital.IsWithinRange(pulseRate) :
                pulseVital.IsWithinRange(pulseRate, profile);
        }

        private bool IsOxygenSaturationWithinRange(float oxygenSaturation, PatientProfile profile) {
            var oxygenVital = VitalSignFactory.Create(VitalSignType.OxygenSaturation);
            return profile == null ?
                oxygenVital.IsWithinRange(oxygenSaturation) :
                oxygenVital.IsWithinRange(oxygenSaturation, profile);
        }

        private void CheckTemperature(float temperature, PatientProfile profile) {
            if (!IsTemperatureWithinRange(temperature, profile)) {
                _alerter.Alert("Temperature critical!");
            }
        }

        private void CheckPulseRate(int pulseRate, PatientProfile profile) {
            if (!IsPulseRateWithinRange(pulseRate, profile)) {
                _alerter.Alert("Pulse Rate critical!");
            }
        }

        private void CheckOxygenSaturation(float oxygenSaturation, PatientProfile profile) {
            if (!IsOxygenSaturationWithinRange(oxygenSaturation, profile)) {
                _alerter.Alert("Oxygen Saturation critical!");
            }
        }
    }
}
