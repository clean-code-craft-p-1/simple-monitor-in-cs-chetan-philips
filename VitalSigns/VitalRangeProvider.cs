using System.Collections.Generic;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Centralized provider for vital sign range configurations.
    /// Simplifies range definitions by storing them in a central location.
    /// </summary>
    public class VitalRangeProvider {
        private readonly Dictionary<string, VitalRangeConfig> _configurations;

        /// <summary>
        /// Creates a new provider with the default vital sign configurations
        /// </summary>
        public VitalRangeProvider() {
            _configurations = CreateDefaultConfigurations();
        }

        /// <summary>
        /// Gets a configuration for the specified vital sign
        /// </summary>
        /// <param name="vitalSignName">Name of the vital sign</param>
        /// <returns>The configuration for the vital sign</returns>
        public VitalRangeConfig GetConfig(string vitalSignName) {
            return _configurations.TryGetValue(vitalSignName, out var config)
                ? config
                : throw new KeyNotFoundException($"No configuration found for {vitalSignName}");
        }

        /// <summary>
        /// Creates the default vital sign configurations
        /// </summary>
        private static Dictionary<string, VitalRangeConfig> CreateDefaultConfigurations() {
            var configs = new Dictionary<string, VitalRangeConfig> {
                {
                    "Temperature", new VitalRangeConfig(
                        "Temperature",
                        "°F",
                        new AgeBasedRanges(
                            (VitalRangeConstants.TEMP_MIN_CHILD, VitalRangeConstants.TEMP_MAX_CHILD),
                            (VitalRangeConstants.TEMP_MIN_ELDERLY, VitalRangeConstants.TEMP_MAX_ELDERLY),
                            (VitalRangeConstants.TEMP_MIN_ADULT, VitalRangeConstants.TEMP_MAX_ADULT)
                        )
                    )
                },
                {
                    "Pulse Rate", new VitalRangeConfig(
                        "Pulse Rate",
                        "BPM",
                        new AgeBasedRanges(
                            (VitalRangeConstants.PULSE_MIN_CHILD, VitalRangeConstants.PULSE_MAX_CHILD),
                            (VitalRangeConstants.PULSE_MIN_ELDERLY, VitalRangeConstants.PULSE_MAX_ELDERLY),
                            (VitalRangeConstants.PULSE_MIN_ADULT, VitalRangeConstants.PULSE_MAX_ADULT)
                        )
                    )
                },
                {
                    "Oxygen Saturation", new VitalRangeConfig(
                        "Oxygen Saturation",
                        "%",
                        new AgeBasedRanges(
                            (VitalRangeConstants.OXY_MIN, VitalRangeConstants.OXY_MAX),
                            (VitalRangeConstants.OXY_MIN, VitalRangeConstants.OXY_MAX),
                            (VitalRangeConstants.OXY_MIN, VitalRangeConstants.OXY_MAX)
                        ),
                        VitalRangeConstants.GetCopdConditionRanges()
                    )
                },
                {
                    "Systolic Blood Pressure", new VitalRangeConfig(
                        "Systolic Blood Pressure",
                        "mmHg",
                        new AgeBasedRanges(
                            (VitalRangeConstants.SYS_MIN_CHILD, VitalRangeConstants.SYS_MAX_CHILD),
                            (VitalRangeConstants.SYS_MIN_ELDERLY, VitalRangeConstants.SYS_MAX_ELDERLY),
                            (VitalRangeConstants.SYS_MIN_ADULT, VitalRangeConstants.SYS_MAX_ADULT)
                        )
                    )
                },
                {
                    "Diastolic Blood Pressure", new VitalRangeConfig(
                        "Diastolic Blood Pressure",
                        "mmHg",
                        new AgeBasedRanges(
                            (VitalRangeConstants.DIA_MIN_CHILD, VitalRangeConstants.DIA_MAX_CHILD),
                            (VitalRangeConstants.DIA_MIN_ELDERLY, VitalRangeConstants.DIA_MAX_ELDERLY),
                            (VitalRangeConstants.DIA_MIN_ADULT, VitalRangeConstants.DIA_MAX_ADULT)
                        )
                    )
                },
                {
                    "Respiratory Rate", new VitalRangeConfig(
                        "Respiratory Rate",
                        "breaths/min",
                        new AgeBasedRanges(
                            (VitalRangeConstants.RESP_MIN_CHILD, VitalRangeConstants.RESP_MAX_CHILD),
                            (VitalRangeConstants.RESP_MIN_ELDERLY, VitalRangeConstants.RESP_MAX_ELDERLY),
                            (VitalRangeConstants.RESP_MIN_ADULT, VitalRangeConstants.RESP_MAX_ADULT)
                        )
                    )
                }
            };

            return configs;
        }
    }
}
