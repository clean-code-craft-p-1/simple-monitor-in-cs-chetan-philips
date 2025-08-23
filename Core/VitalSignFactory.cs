using System;
using System.Collections.Generic;

using HealthMonitor.VitalSigns;

namespace HealthMonitor.Core {
    /// <summary>
    /// Factory for creating vital sign implementations.
    /// Provides centralized creation and management of vital sign types.
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
            OxygenSaturation,
            /// <summary>Blood pressure</summary>
            BloodPressure
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
                VitalSignType.BloodPressure => new SystolicBloodPressure(),
                _ => throw new ArgumentException($"Unknown vital sign type: {type}", nameof(type))
            };
        }

        /// <summary>
        /// Creates a vital sign checker instance based on the specified name.
        /// </summary>
        /// <param name="name">The name of the vital sign to create</param>
        /// <returns>An instance implementing IVitalSign</returns>
        /// <exception cref="ArgumentException">Thrown when an unknown vital sign name is provided</exception>
        public static IVitalSign Create(string name) {
            return name switch {
                "Temperature" => new VitalSigns.Temperature(),
                "Pulse Rate" => new VitalSigns.PulseRate(),
                "Oxygen Saturation" => new VitalSigns.OxygenSaturation(),
                "Blood Pressure" => new VitalSigns.SystolicBloodPressure(),
                _ => throw new ArgumentException($"Unknown vital sign: {name}")
            };
        }

        /// <summary>
        /// Creates all standard vital sign checkers as an enumerable.
        /// </summary>
        /// <returns>Enumerable of all standard vital sign implementations</returns>
        public static IEnumerable<IVitalSign> CreateAll() {
            yield return new VitalSigns.Temperature();
            yield return new VitalSigns.PulseRate();
            yield return new VitalSigns.OxygenSaturation();
            yield return new VitalSigns.SystolicBloodPressure();
        }
    }
}