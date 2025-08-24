namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Generic implementation of an age-based vital sign that accepts parameters through constructor
    /// to eliminate code duplication across vital sign classes.
    /// </summary>
    public class StandardAgeBasedVitalSign : AgeBasedVitalSign {
        private readonly string _name;
        private readonly string _unit;
        private readonly AgeRanges _ranges;

        /// <summary>
        /// Creates a new vital sign with the specified name, unit, and age ranges
        /// </summary>
        public StandardAgeBasedVitalSign(string name, string unit, AgeRanges ranges) {
            _name = name;
            _unit = unit;
            _ranges = ranges;
        }

        public override string Name => _name;
        public override string Unit => _unit;

        protected override (float min, float max) GetAgeSpecificRange(int? age) {
            return AgeBasedRangeHelper.GetRangeByAge(age, _ranges);
        }
    }
}
