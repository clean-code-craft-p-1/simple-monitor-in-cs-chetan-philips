namespace HealthMonitor.Core {
    /// <summary>
    /// Represents a complete set of vital sign readings taken at a specific time.
    /// Encapsulates temperature, pulse rate, and oxygen saturation measurements.
    /// </summary>
    public class VitalReading {
        /// <summary>
        /// Gets the body temperature in Fahrenheit.
        /// </summary>
        public float Temperature { get; }

        /// <summary>
        /// Gets the pulse rate in beats per minute.
        /// </summary>
        public int PulseRate { get; }

        /// <summary>
        /// Gets the oxygen saturation percentage (SpO2).
        /// </summary>
        public float OxygenSaturation { get; }

        /// <summary>
        /// Initializes a new instance of VitalReading with the specified measurements.
        /// </summary>
        /// <param name="temperature">Body temperature in Fahrenheit (normal range: 95-102°F)</param>
        /// <param name="pulseRate">Pulse rate in beats per minute (normal range: 60-100 BPM)</param>
        /// <param name="oxygenSaturation">Oxygen saturation percentage (normal range: 90-100%)</param>
        public VitalReading(float temperature, int pulseRate, float oxygenSaturation) {
            Temperature = temperature;
            PulseRate = pulseRate;
            OxygenSaturation = oxygenSaturation;
        }
    }
}