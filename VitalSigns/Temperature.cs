using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Temperature vital sign checker with age-based adjustments.
    /// </summary>
    public class Temperature : AgeBasedVitalSign {
        public override string Name => "Temperature";
        public override string Unit => "°F";

        protected override (float min, float max) GetAgeSpecificRange(int? age) {
            return AgeBasedRangeHelper.GetRangeByAge(age,
                (VitalRangeConstants.TEMP_MIN_CHILD, VitalRangeConstants.TEMP_MAX_CHILD),
                (VitalRangeConstants.TEMP_MIN_ELDERLY, VitalRangeConstants.TEMP_MAX_ELDERLY),
                (VitalRangeConstants.TEMP_MIN_ADULT, VitalRangeConstants.TEMP_MAX_ADULT)
            );
        }
    }
}