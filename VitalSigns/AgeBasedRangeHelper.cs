namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Helper class to provide age-based vital ranges and eliminate duplication
    /// in derived vital sign classes.
    /// </summary>
    public static class AgeBasedRangeHelper {
        public static (float min, float max) GetRangeByAge(int? age,
            (float min, float max) childRange,
            (float min, float max) elderlyRange,
            (float min, float max) adultRange) {
            return age switch {
                < VitalRangeConstants.CHILD_AGE_THRESHOLD => childRange,     // Child
                >= VitalRangeConstants.ELDERLY_AGE_THRESHOLD => elderlyRange,  // Elderly
                _ => adultRange         // Adult
            };
        }
    }
}
