using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Systolic blood pressure vital sign checker.
    /// </summary>
    public class SystolicBloodPressure : StandardAgeBasedVitalSign {
        public SystolicBloodPressure() : base(
            "Systolic Blood Pressure",
            "mmHg",
            (VitalRangeConstants.SYS_MIN_CHILD, VitalRangeConstants.SYS_MAX_CHILD),
            (VitalRangeConstants.SYS_MIN_ELDERLY, VitalRangeConstants.SYS_MAX_ELDERLY),
            (VitalRangeConstants.SYS_MIN_ADULT, VitalRangeConstants.SYS_MAX_ADULT)
        ) { }
    }
}