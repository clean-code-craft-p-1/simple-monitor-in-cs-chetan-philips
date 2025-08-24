namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Temperature vital sign checker with age-based adjustments.
    /// </summary>
    public class Temperature : StandardAgeBasedVitalSign {
        public Temperature() : base(
            "Temperature",
            "°F",
            new AgeRanges(
                (VitalRangeConstants.TEMP_MIN_CHILD, VitalRangeConstants.TEMP_MAX_CHILD),
                (VitalRangeConstants.TEMP_MIN_ELDERLY, VitalRangeConstants.TEMP_MAX_ELDERLY),
                (VitalRangeConstants.TEMP_MIN_ADULT, VitalRangeConstants.TEMP_MAX_ADULT)
            )
        ) { }
    }
}