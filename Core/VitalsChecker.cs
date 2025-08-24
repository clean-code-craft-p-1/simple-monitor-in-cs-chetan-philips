using System.Collections.Generic;
using HealthMonitor.Models;

namespace HealthMonitor.Core {
    /// <summary>
    /// Extensible vital signs checker with runtime registration.
    /// </summary>
    public class VitalsChecker {
        private readonly IVitalSignAlerter _alerter;
        private readonly Dictionary<string, IVitalSign> _vitalSigns = new();

        public VitalsChecker(IVitalSignAlerter alerter) {
            _alerter = alerter;
            RegisterDefaultVitalSigns();
        }

        public void CheckVitals(VitalReading vitals, PatientProfile profile = null) {
            foreach (var vitalName in vitals.GetVitalNames()) {
                CheckVital(vitalName, vitals.GetReading(vitalName), profile);
            }
        }

        public bool AreAllVitalsWithinRange(VitalReading vitals, PatientProfile profile = null) {
            foreach (var vitalName in vitals.GetVitalNames()) {
                if (!IsVitalWithinRange(vitalName, vitals.GetReading(vitalName), profile)) {
                    return false;
                }
            }
            return true;
        }

        public void RegisterVitalSign(IVitalSign vitalSign) {
            _vitalSigns[vitalSign.Name] = vitalSign;
        }

        private void CheckVital(string vitalName, float value, PatientProfile profile) {
            if (_vitalSigns.TryGetValue(vitalName, out var vitalSign)) {
                if (!vitalSign.IsWithinRange(value, profile)) {
                    _alerter.Alert(vitalName, value.ToString("F1"), vitalSign.Unit);
                }
            }
        }

        private bool IsVitalWithinRange(string vitalName, float value, PatientProfile profile) {
            return !_vitalSigns.TryGetValue(vitalName, out var vitalSign) ||
                   vitalSign.IsWithinRange(value, profile);
        }

        private void RegisterDefaultVitalSigns() {
            RegisterVitalSign(new VitalSigns.Temperature());
            RegisterVitalSign(new VitalSigns.PulseRate());
            RegisterVitalSign(new VitalSigns.OxygenSaturation());
            RegisterVitalSign(new VitalSigns.SystolicBloodPressure());
            RegisterVitalSign(new VitalSigns.DiastolicBloodPressure());
        }
    }
}