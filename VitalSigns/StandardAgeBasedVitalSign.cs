namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Generic implementation of an age-based vital sign that accepts parameters through constructor
    /// to eliminate code duplication across vital sign classes.
    /// </summary>
    public class StandardAgeBasedVitalSign : AgeBasedVitalSign {
        private readonly string _name;
        private readonly string _unit;
        private readonly (float min, float max) _childRange;
        private readonly (float min, float max) _elderlyRange;
        private readonly (float min, float max) _adultRange;

        public StandardAgeBasedVitalSign(
            string name,
            string unit,
            (float min, float max) childRange,
            (float min, float max) elderlyRange,
            (float min, float max) adultRange) {
            _name = name;
            _unit = unit;
            _childRange = childRange;
            _elderlyRange = elderlyRange;
            _adultRange = adultRange;
        }

        public override string Name => _name;
        public override string Unit => _unit;

        protected override (float min, float max) GetAgeSpecificRange(int? age) {
            return AgeBasedRangeHelper.GetRangeByAge(age, _childRange, _elderlyRange, _adultRange);
        }
    }
}
