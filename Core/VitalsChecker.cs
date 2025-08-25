using System;
using System.Collections.Generic;

using HealthMonitor.Models;
using HealthMonitor.VitalSigns;

namespace HealthMonitor.Core {
    /// <summary>
    /// Simplified vital signs checker using action delegate for alerting.
    /// </summary>
    public class VitalsChecker {
        private readonly Action<string, string, string> _alertAction;
        private readonly Dictionary<string, IVitalSign> _vitalSigns = new();

        public VitalsChecker(Action<string, string, string> alertAction = null) {
            _alertAction = alertAction ?? DefaultAlert;
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

        private static void DefaultAlert(string vitalName, string value, string unit) {
            Console.WriteLine($"ALERT: {vitalName} is {value} {unit}");
        }

        private void CheckVital(string vitalName, float value, PatientProfile profile) {
            if (_vitalSigns.TryGetValue(vitalName, out var vitalSign)) {
                if (!vitalSign.IsWithinRange(value, profile)) {
                    _alertAction(vitalName, value.ToString("F1"), vitalSign.Unit);
                }
            }
        }

        private bool IsVitalWithinRange(string vitalName, float value, PatientProfile profile) {
            return !_vitalSigns.TryGetValue(vitalName, out var vitalSign) ||
                   vitalSign.IsWithinRange(value, profile);
        }

        private void RegisterDefaultVitalSigns() {
            // Use the factory to create vital signs with configurations
            var vitalSigns = VitalSignFactory.CreateStandardVitalSigns();
            foreach (var vitalSign in vitalSigns.Values) {
                RegisterVitalSign(vitalSign);
            }
        }
    }
}