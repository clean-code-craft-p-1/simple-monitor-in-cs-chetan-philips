using System;

using HealthMonitor.VitalSigns;

namespace HealthMonitor.Core {
    /// <summary>
    /// Factory for creating vital sign implementations.
    /// Provides centralized creation of vital sign checkers.
    /// </summary>
    public static class VitalSignFactory {
        /// <summary>
        /// Enumeration of supported vital sign types.
        /// </summary>
        public enum VitalSignType {
            /// <summary>Body temperature</summary>
            Temperature,
            /// <summary>Heart pulse rate</summary>
            PulseRate,
            /// <summary>Blood oxygen saturation</summary>
            OxygenSaturation
        }

        /// <summary>
        /// Creates a vital sign checker instance based on the specified type.
        /// </summary>
        /// <param name="type">The type of vital sign to create</param>
        /// <returns>An instance implementing IVitalSign</returns>
        /// <exception cref="ArgumentException">Thrown when an unknown vital sign type is provided</exception>
        public static IVitalSign Create(VitalSignType type) {
            return type switch {
                VitalSignType.Temperature => new Temperature(),
                VitalSignType.PulseRate => new PulseRate(),
                VitalSignType.OxygenSaturation => new OxygenSaturation(),
                _ => throw new ArgumentException($"Unknown vital sign type: {type}", nameof(type))
            };
        }

        /// <summary>
        /// Creates all standard vital sign checkers.
        /// </summary>
        /// <returns>Array of all standard vital sign implementations</returns>
        public static IVitalSign[] CreateAll() {
            return new IVitalSign[]
            {
                new Temperature(),
                new PulseRate(),
                new OxygenSaturation()
            };
        }
    }
}