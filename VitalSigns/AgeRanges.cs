namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Structure to hold age-specific ranges to avoid parameter duplication
    /// </summary>
    public struct AgeRanges {
        public (float min, float max) ChildRange { get; set; }
        public (float min, float max) ElderlyRange { get; set; }
        public (float min, float max) AdultRange { get; set; }

        public AgeRanges(
            (float min, float max) childRange,
            (float min, float max) elderlyRange,
            (float min, float max) adultRange) {
            ChildRange = childRange;
            ElderlyRange = elderlyRange;
            AdultRange = adultRange;
        }
    }
}
