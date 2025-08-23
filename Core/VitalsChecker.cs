using System.Collections.Generic;
using HealthMonitor.Models;

namespace HealthMonitor.Core {
    /// <summary>
    /// Simple, extensible vital signs checker.
    /// Automatically discovers and checks all available vital signs.
    /// </summary>
    public class VitalsChecker {
        private readonly IVitalSignAlerter _alerter;
        private readonly Dictionary<string, IVitalSign> _vitalCheckers;

        public VitalsChecker(IVitalSignAlerter alerter) {
            _alerter = alerter;
            _vitalCheckers = CreateStandardVitalCheckers();
        }

        public void CheckVitals(VitalReading vitals, PatientProfile profile = null) {
            foreach (var vitalName in vitals.GetVitalNames()) {
                CheckSingleVital(vitalName, vitals.GetReading(vitalName), profile);
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

        // Easy way to add new vital signs at runtime
        public void RegisterVitalSign(IVitalSign vitalSign) {
            _vitalCheckers[vitalSign.Name] = vitalSign;
        }

        private void CheckSingleVital(string vitalName, float value, PatientProfile profile) {
            if (!IsVitalWithinRange(vitalName, value, profile)) {
                var unit = _vitalCheckers.TryGetValue(vitalName, out var checker) ? checker.Unit : "";
                _alerter.Alert(vitalName, value.ToString("F1"), unit);
            }
        }

        private bool IsVitalWithinRange(string vitalName, float value, PatientProfile profile) {
            return _vitalCheckers.TryGetValue(vitalName, out var checker) &&
                   checker.IsWithinRange(value, profile);
        }

        private static Dictionary<string, IVitalSign> CreateStandardVitalCheckers() {
            var checkers = new Dictionary<string, IVitalSign>();

            // Register standard vital signs
            var standardVitals = new IVitalSign[] {
                new VitalSigns.Temperature(),
                new VitalSigns.PulseRate(),
                new VitalSigns.OxygenSaturation(),
                new VitalSigns.SystolicBloodPressure(),
                new VitalSigns.DiastolicBloodPressure()
            };

            foreach (var vital in standardVitals) {
                checkers[vital.Name] = vital;
            }

            return checkers;
        }
    }
}