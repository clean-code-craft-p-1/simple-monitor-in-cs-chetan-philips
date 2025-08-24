namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Helper class to provide age-based vital ranges and eliminate duplication
    /// in derived vital sign classes.
    /// </summary>
    public static class AgeBasedRangeHelper {
        public static (float min, float max) GetRangeByAge(int? age, AgeRanges ranges) {
            return age switch {
                < VitalRangeConstants.CHILD_AGE_THRESHOLD => ranges.ChildRange,     // Child
                >= VitalRangeConstants.ELDERLY_AGE_THRESHOLD => ranges.ElderlyRange,  // Elderly
                _ => ranges.AdultRange         // Adult
            };
        }
        
        // Keeping this for backward compatibility
        public static (float min, float max) GetRangeByAge(
            int? age,
            (float min, float max) childRange,
            (float min, float max) elderlyRange,
            (float min, float max) adultRange) {
            var ranges = new AgeRanges(childRange, elderlyRange, adultRange);
            return GetRangeByAge(age, ranges);
        }
    }
}
