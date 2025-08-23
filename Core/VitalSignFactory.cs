using System;

using HealthMonitor.VitalSigns;

namespace HealthMonitor.Core {
    /// <summary>
    /// Enum representing different types of vital signs
    /// </summary>
    public enum VitalSignType {
        Temperature,
        PulseRate,
        OxygenSaturation
    }

    /// <summary>
    /// Factory class for creating instances of IVitalSign based on type
    /// </summary>
    public static class VitalSignFactory {
        /// <summary>
        /// Creates an instance of IVitalSign based on the specified type.
        /// </summary>
        /// <param name="type">The type of vital sign to create.</param>
        /// <returns>An instance of IVitalSign corresponding to the specified type.</returns>
        /// <exception cref="ArgumentException">Thrown when an unknown vital sign type is specified.</exception>
        public static IVitalSign Create(VitalSignType type) {
            return type switch {
                VitalSignType.Temperature => new Temperature(),
                VitalSignType.PulseRate => new PulseRate(),
                VitalSignType.OxygenSaturation => new OxygenSaturation(),
                _ => throw new ArgumentException($"Unknown vital sign type: {type}")
            };
        }
    }
}