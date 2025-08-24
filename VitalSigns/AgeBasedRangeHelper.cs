namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Helper class to provide age-based vital ranges and eliminate duplication
    /// in derived vital sign classes.
    /// </summary>
    public static class AgeBasedRangeHelper {
        /// <summary>
        /// Get the appropriate range based on age using the AgeRanges struct
        /// </summary>
        public static (float min, float max) GetRangeByAge(int? age, AgeRanges ranges) {
            return age switch {
                < VitalRangeConstants.CHILD_AGE_THRESHOLD => ranges.ChildRange,     // Child
                >= VitalRangeConstants.ELDERLY_AGE_THRESHOLD => ranges.ElderlyRange,  // Elderly
                _ => ranges.AdultRange         // Adult
            };
        }
    }
}
