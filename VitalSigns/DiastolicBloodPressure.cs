using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Diastolic blood pressure vital sign checker.
    /// </summary>
    public class DiastolicBloodPressure : StandardAgeBasedVitalSign {
        public DiastolicBloodPressure() : base(
            "Diastolic Blood Pressure",
            "mmHg",
            (VitalRangeConstants.DIA_MIN_CHILD, VitalRangeConstants.DIA_MAX_CHILD),
            (VitalRangeConstants.DIA_MIN_ELDERLY, VitalRangeConstants.DIA_MAX_ELDERLY),
            (VitalRangeConstants.DIA_MIN_ADULT, VitalRangeConstants.DIA_MAX_ADULT)
        ) { }
    }
}