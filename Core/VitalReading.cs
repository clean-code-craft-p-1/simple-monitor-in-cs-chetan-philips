namespace HealthMonitor.Core {
    /// <summary>
    /// Represents a vital reading with temperature, pulse rate, and oxygen saturation
    /// </summary>
    public class VitalReading {
        /// <summary>
        /// Gets the temperature in degrees Celsius
        /// </summary>
        public float Temperature { get; }
        /// <summary>
        /// Gets the pulse rate in beats per minute
        /// </summary>
        public int PulseRate { get; }
        /// <summary>
        /// Gets the oxygen saturation percentage
        /// </summary>
        public float OxygenSaturation { get; }

        /// <summary>
        /// Initializes a new instance of the VitalReading class with specified values
        /// </summary>
        /// <param name="temperature">Temperature in degrees Celsius</param>
        /// <param name="pulseRate">Pulse rate in beats per minute</param>
        /// <param name="oxygenSaturation">Oxygen saturation percentage</param>
        public VitalReading(float temperature, int pulseRate, float oxygenSaturation) {
            Temperature = temperature;
            PulseRate = pulseRate;
            OxygenSaturation = oxygenSaturation;
        }
    }
}
