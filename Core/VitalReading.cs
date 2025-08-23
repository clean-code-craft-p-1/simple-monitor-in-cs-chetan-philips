using System;

namespace HealthMonitor.Core {
    /// <summary>
    /// Represents a complete set of vital sign readings including blood pressure.
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
        /// Gets the systolic blood pressure in mmHg.
        /// </summary>
        public float SystolicBloodPressure { get; }

        /// <summary>
        /// Gets the diastolic blood pressure in mmHg.
        /// </summary>
        public float DiastolicBloodPressure { get; }

        /// <summary>
        /// Initializes a new instance of VitalReading with the specified measurements, including blood pressure.
        /// </summary>
        /// <param name="temperature">Body temperature in Fahrenheit (normal range: 95-102�F)</param>
        /// <param name="pulseRate">Pulse rate in beats per minute (normal range: 60-100 BPM)</param>
        /// <param name="oxygenSaturation">Oxygen saturation percentage (normal range: 90-100%)</param>
        /// <param name="systolicBloodPressure">Systolic blood pressure in mmHg</param>
        /// <param name="diastolicBloodPressure">Diastolic blood pressure in mmHg</param>
        public VitalReading(float temperature, int pulseRate, float oxygenSaturation, 
                          float systolicBloodPressure, float diastolicBloodPressure) {
            Temperature = temperature;
            PulseRate = pulseRate;
            OxygenSaturation = oxygenSaturation;
            SystolicBloodPressure = systolicBloodPressure;
            DiastolicBloodPressure = diastolicBloodPressure;
        }

        /// <summary>
        /// Gets a value indicating whether the blood pressure readings are available.
        /// </summary>
        public bool HasBloodPressure => SystolicBloodPressure > 0 && DiastolicBloodPressure > 0;
    }
}