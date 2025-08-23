using System;
using HealthMonitor.Infrastructure;
using HealthMonitor.Models;

namespace HealthMonitor.Core {
    /// <summary>
    /// Main service class for checking vital signs against normal ranges.
    /// Coordinates between vital sign checkers and alerting systems.
    /// </summary>
    public class VitalsChecker {
        private readonly IVitalSignAlerter _alerter;
        private readonly IVitalSign[] _vitalSigns;

        /// <summary>
        /// Initializes a new instance of VitalsChecker with the specified alerter.
        /// </summary>
        /// <param name="alerter">The alerting system to use for out-of-range notifications</param>
        public VitalsChecker(IVitalSignAlerter alerter) {
            _alerter = alerter ?? throw new ArgumentNullException(nameof(alerter));
            _vitalSigns = VitalSignFactory.CreateAll();
        }

        /// <summary>
        /// Checks all vital signs and triggers alerts for any out-of-range values.
        /// </summary>
        /// <param name="vitals">The vital readings to check</param>
        /// <param name="profile">Optional patient profile for personalized checking</param>
        public void CheckVitals(VitalReading vitals, PatientProfile profile = null) {
            if (vitals == null)
                throw new ArgumentNullException(nameof(vitals));

            CheckTemperature(vitals.Temperature, profile);
            CheckPulseRate(vitals.PulseRate, profile);
            CheckOxygenSaturation(vitals.OxygenSaturation, profile);
        }

        /// <summary>
        /// Checks if all vital signs are within normal ranges without triggering alerts.
        /// Pure function for testing and validation purposes.
        /// </summary>
        /// <param name="vitals">The vital readings to check</param>
        /// <param name="profile">Optional patient profile for personalized checking</param>
        /// <returns>True if all vitals are within range, false otherwise</returns>
        public bool AreAllVitalsWithinRange(VitalReading vitals, PatientProfile profile = null) {
            if (vitals == null)
                return false;

            foreach (var vitalSign in _vitalSigns) {
                var value = GetVitalValue(vitals, vitalSign.Name);
                if (!vitalSign.IsWithinRange(value, profile))
                    return false;
            }
            return true;
        }

        private void CheckTemperature(float temperature, PatientProfile profile) {
            var tempChecker = GetVitalSignByName("Temperature");
            if (!tempChecker.IsWithinRange(temperature, profile)) {
                _alerter.Alert(tempChecker.Name, temperature, tempChecker.Unit);
            }
        }

        private void CheckPulseRate(int pulseRate, PatientProfile profile) {
            var pulseChecker = GetVitalSignByName("Pulse Rate");
            if (!pulseChecker.IsWithinRange(pulseRate, profile)) {
                _alerter.Alert(pulseChecker.Name, pulseRate, pulseChecker.Unit);
            }
        }

        private void CheckOxygenSaturation(float oxygenSaturation, PatientProfile profile) {
            var oxygenChecker = GetVitalSignByName("Oxygen Saturation");
            if (!oxygenChecker.IsWithinRange(oxygenSaturation, profile)) {
                _alerter.Alert(oxygenChecker.Name, oxygenSaturation, oxygenChecker.Unit);
            }
        }

        private IVitalSign GetVitalSignByName(string name) {
            foreach (var vitalSign in _vitalSigns) {
                if (vitalSign.Name == name)
                    return vitalSign;
            }
            throw new InvalidOperationException($"Vital sign '{name}' not found");
        }

        private float GetVitalValue(VitalReading vitals, string vitalName) {
            return vitalName switch {
                "Temperature" => vitals.Temperature,
                "Pulse Rate" => vitals.PulseRate,
                "Oxygen Saturation" => vitals.OxygenSaturation,
                _ => throw new ArgumentException($"Unknown vital sign: {vitalName}")
            };
        }
    }
}