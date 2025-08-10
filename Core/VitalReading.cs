namespace HealthMonitor.Core {
    /// <summary>
    /// Class that represents a vital reading with its value and corresponding vital sign
    /// </summary>
    public class VitalReading {
        /// <summary>Gets the vital sign associated with this reading</summary>
        public IVitalSign VitalSign { get; }

        /// <summary>Gets the measured value of the vital sign</summary>
        /// <summary>Gets the measured value of the vital sign</summary>
        public float Value { get; }

        /// <summary>Gets whether this reading is within normal range</summary>
        public bool IsWithinRange => VitalSign.IsWithinRange(Value);

        /// <summary>
        /// Creates a new vital reading
        /// </summary>
        /// <param name="vitalSign">The vital sign type</param>
        /// <param name="value">The measured value</param>
        public VitalReading(IVitalSign vitalSign, float value) {
            VitalSign = vitalSign;
            Value = value;
        }
    }
}
