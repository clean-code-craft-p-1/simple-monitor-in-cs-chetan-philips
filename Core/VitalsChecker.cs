using System;
using System.Collections.Generic;
using HealthMonitor.Infrastructure;
using HealthMonitor.Models;

namespace HealthMonitor.Core {
    /// <summary>
    /// Core vital signs checker that validates readings against normal ranges.
    /// Supports extensible vital sign checking with patient-specific adjustments.
    /// </summary>
    public class VitalsChecker {
        private readonly IVitalSignAlerter _alerter;
        private readonly IEnumerable<IVitalSign> _vitalSigns;

        public VitalsChecker(IVitalSignAlerter alerter) {
            _alerter = alerter;
            _vitalSigns = VitalSignFactory.CreateAll();
        }

        public void CheckVitals(VitalReading vitals, PatientProfile profile = null) {
            CheckTemperature(vitals.Temperature, profile);
            CheckPulseRate(vitals.PulseRate, profile);
            CheckOxygenSaturation(vitals.OxygenSaturation, profile);
            
            if (vitals.HasBloodPressure) {
                CheckBloodPressure(vitals.SystolicBloodPressure, vitals.DiastolicBloodPressure, profile);
            }
        }

        public bool AreAllVitalsWithinRange(VitalReading vitals, PatientProfile profile = null) {
            bool temperatureOk = CheckTemperature(vitals.Temperature, profile);
            bool pulseRateOk = CheckPulseRate(vitals.PulseRate, profile);
            bool oxygenSatOk = CheckOxygenSaturation(vitals.OxygenSaturation, profile);
            
            if (vitals.HasBloodPressure) {
                bool bloodPressureOk = CheckBloodPressure(vitals.SystolicBloodPressure, vitals.DiastolicBloodPressure, profile);
                return temperatureOk && pulseRateOk && oxygenSatOk && bloodPressureOk;
            }
            
            return temperatureOk && pulseRateOk && oxygenSatOk;
        }

        private bool CheckTemperature(float temperature, PatientProfile profile) {
            var tempVitalSign = GetVitalSignByName("Temperature");
            bool isNormal = tempVitalSign.IsWithinRange(temperature, profile);

            if (!isNormal) {
                _alerter.Alert("Temperature", temperature.ToString("F1"), tempVitalSign.Unit);
            }

            return isNormal;
        }

        private bool CheckPulseRate(float pulseRate, PatientProfile profile) {
            var pulseVitalSign = GetVitalSignByName("Pulse Rate");
            bool isNormal = pulseVitalSign.IsWithinRange(pulseRate, profile);

            if (!isNormal) {
                _alerter.Alert("Pulse Rate", pulseRate.ToString("F0"), pulseVitalSign.Unit);
            }

            return isNormal;
        }

        private bool CheckOxygenSaturation(float oxygenSaturation, PatientProfile profile) {
            var oxygenVitalSign = GetVitalSignByName("Oxygen Saturation");
            bool isNormal = oxygenVitalSign.IsWithinRange(oxygenSaturation, profile);

            if (!isNormal) {
                _alerter.Alert("Oxygen Saturation", oxygenSaturation.ToString("F1"), oxygenVitalSign.Unit);
            }

            return isNormal;
        }

        private bool CheckBloodPressure(float systolic, float diastolic, PatientProfile profile) {
            var bloodPressure = GetVitalSignByName("Blood Pressure") as VitalSigns.BloodPressure;
            bool isNormal = bloodPressure?.IsWithinRange(systolic, diastolic, profile) ?? true;

            if (!isNormal) {
                _alerter.Alert("Blood Pressure", $"{systolic:F0}/{diastolic:F0}", bloodPressure?.Unit ?? "mmHg");
            }

            return isNormal;
        }

        private IVitalSign GetVitalSignByName(string name) {
            foreach (var vitalSign in _vitalSigns) {
                if (vitalSign.Name == name) {
                    return vitalSign;
                }
            }
            return VitalSignFactory.Create(name);
        }
    }
}