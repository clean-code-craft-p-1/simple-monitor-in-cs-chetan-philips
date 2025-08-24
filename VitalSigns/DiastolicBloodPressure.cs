using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Diastolic blood pressure vital sign checker.
    /// </summary>
    public class DiastolicBloodPressure : AgeBasedVitalSign {
        public override string Name => "Diastolic Blood Pressure";
        public override string Unit => "mmHg";

        protected override (float min, float max) GetAgeSpecificRange(int? age) {
            return AgeBasedRangeHelper.GetRangeByAge(age,
                (VitalRangeConstants.DIA_MIN_CHILD, VitalRangeConstants.DIA_MAX_CHILD),
                (VitalRangeConstants.DIA_MIN_ELDERLY, VitalRangeConstants.DIA_MAX_ELDERLY),
                (VitalRangeConstants.DIA_MIN_ADULT, VitalRangeConstants.DIA_MAX_ADULT)
            );
        }
    }
}