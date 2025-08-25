namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Manages age-specific vital sign ranges
    /// </summary>
    public class AgeBasedRanges {
        public (float min, float max) ChildRange { get; }
        public (float min, float max) ElderlyRange { get; }
        public (float min, float max) AdultRange { get; }

        public AgeBasedRanges(
            (float min, float max) childRange,
            (float min, float max) adultRange,
            (float min, float max) elderlyRange) {
            ChildRange = childRange;
            AdultRange = adultRange;
            ElderlyRange = elderlyRange;
        }

        public (float min, float max) GetRangeForAge(int? age) {
            return age switch {
                < VitalRangeConstants.CHILD_AGE_THRESHOLD => ChildRange,
                >= VitalRangeConstants.ELDERLY_AGE_THRESHOLD => ElderlyRange,
                _ => AdultRange
            };
        }
    }
}