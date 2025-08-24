using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Systolic blood pressure vital sign checker.
    /// </summary>
    public class SystolicBloodPressure : AgeBasedVitalSign {
        public override string Name => "Systolic Blood Pressure";
        public override string Unit => "mmHg";

        protected override (float min, float max) GetAgeSpecificRange(int? age) {
            return AgeBasedRangeHelper.GetRangeByAge(age,
                (VitalRangeConstants.SYS_MIN_CHILD, VitalRangeConstants.SYS_MAX_CHILD),
                (VitalRangeConstants.SYS_MIN_ELDERLY, VitalRangeConstants.SYS_MAX_ELDERLY),
                (VitalRangeConstants.SYS_MIN_ADULT, VitalRangeConstants.SYS_MAX_ADULT)
            );
        }
    }
}