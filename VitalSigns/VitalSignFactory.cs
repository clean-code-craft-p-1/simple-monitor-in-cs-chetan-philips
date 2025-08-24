using System.Collections.Generic;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Factory for creating vital sign implementations based on configurations.
    /// Reduces inheritance depth by using composition over inheritance.
    /// </summary>
    public static class VitalSignFactory {
        /// <summary>
        /// Creates a vital sign based on the provided configuration
        /// </summary>
        /// <param name="config">Configuration for the vital sign</param>
        /// <returns>An implementation of IVitalSign</returns>
        public static IVitalSign CreateVitalSign(VitalRangeConfig config) {
            return new ConfigurableVitalSign(config);
        }

        /// <summary>
        /// Creates standard vital signs with default configurations
        /// </summary>
        /// <returns>A read-only dictionary of vital signs keyed by name</returns>
        public static IReadOnlyDictionary<string, IVitalSign> CreateStandardVitalSigns() {
            var result = new Dictionary<string, IVitalSign> {
                { "Temperature", CreateTemperature() },
                { "Pulse Rate", CreatePulseRate() },
                { "Oxygen Saturation", CreateOxygenSaturation() },
                { "Systolic Blood Pressure", CreateSystolicBP() },
                { "Diastolic Blood Pressure", CreateDiastolicBP() }
            };
            return result;
        }

        /// <summary>
        /// Creates a Temperature vital sign
        /// </summary>
        public static IVitalSign CreateTemperature() => 
            CreateVitalSign(new VitalSignParameters(
                "Temperature", 
                "°F",
                VitalRangeConstants.TEMP_MIN_CHILD, VitalRangeConstants.TEMP_MAX_CHILD,
                VitalRangeConstants.TEMP_MIN_ADULT, VitalRangeConstants.TEMP_MAX_ADULT,
                VitalRangeConstants.TEMP_MIN_ELDERLY, VitalRangeConstants.TEMP_MAX_ELDERLY));

        /// <summary>
        /// Creates a Pulse Rate vital sign
        /// </summary>
        public static IVitalSign CreatePulseRate() => 
            CreateVitalSign(new VitalSignParameters(
                "Pulse Rate", 
                "BPM",
                VitalRangeConstants.PULSE_MIN_CHILD, VitalRangeConstants.PULSE_MAX_CHILD,
                VitalRangeConstants.PULSE_MIN_ADULT, VitalRangeConstants.PULSE_MAX_ADULT,
                VitalRangeConstants.PULSE_MIN_ELDERLY, VitalRangeConstants.PULSE_MAX_ELDERLY));

        /// <summary>
        /// Creates an Oxygen Saturation vital sign
        /// </summary>
        public static IVitalSign CreateOxygenSaturation() {
            var parameters = new VitalSignParameters(
                "Oxygen Saturation",
                "%",
                VitalRangeConstants.OXY_MIN, VitalRangeConstants.OXY_MAX,
                VitalRangeConstants.OXY_MIN, VitalRangeConstants.OXY_MAX,
                VitalRangeConstants.OXY_MIN, VitalRangeConstants.OXY_MAX
            );
            
            var conditionRanges = VitalRangeConstants.GetCopdConditionRanges();

            return CreateVitalSignWithConditions(parameters, conditionRanges);
        }

        /// <summary>
        /// Creates a Systolic Blood Pressure vital sign
        /// </summary>
        public static IVitalSign CreateSystolicBP() => 
            CreateVitalSign(new VitalSignParameters(
                "Systolic Blood Pressure", 
                "mmHg",
                VitalRangeConstants.SYS_MIN_CHILD, VitalRangeConstants.SYS_MAX_CHILD,
                VitalRangeConstants.SYS_MIN_ADULT, VitalRangeConstants.SYS_MAX_ADULT,
                VitalRangeConstants.SYS_MIN_ELDERLY, VitalRangeConstants.SYS_MAX_ELDERLY));

        /// <summary>
        /// Creates a Diastolic Blood Pressure vital sign
        /// </summary>
        public static IVitalSign CreateDiastolicBP() => 
            CreateVitalSign(new VitalSignParameters(
                "Diastolic Blood Pressure", 
                "mmHg",
                VitalRangeConstants.DIA_MIN_CHILD, VitalRangeConstants.DIA_MAX_CHILD,
                VitalRangeConstants.DIA_MIN_ADULT, VitalRangeConstants.DIA_MAX_ADULT,
                VitalRangeConstants.DIA_MIN_ELDERLY, VitalRangeConstants.DIA_MAX_ELDERLY));

        /// <summary>
        /// Creates a Respiratory Rate vital sign
        /// </summary>
        public static IVitalSign CreateRespiratoryRate() => 
            CreateVitalSign(new VitalSignParameters(
                "Respiratory Rate", 
                "breaths/min",
                VitalRangeConstants.RESP_MIN_CHILD, VitalRangeConstants.RESP_MAX_CHILD,
                VitalRangeConstants.RESP_MIN_ADULT, VitalRangeConstants.RESP_MAX_ADULT,
                VitalRangeConstants.RESP_MIN_ELDERLY, VitalRangeConstants.RESP_MAX_ELDERLY));
                
        /// <summary>
        /// Helper method to create a vital sign with age-based ranges
        /// </summary>
        private static IVitalSign CreateVitalSign(VitalSignParameters parameters) {            
            return new ConfigurableVitalSign(
                CreateBaseConfig(parameters)
            );
        }
        
        /// <summary>
        /// Helper method to create a vital sign with age-based and condition-based ranges
        /// </summary>
        private static IVitalSign CreateVitalSignWithConditions(
            VitalSignParameters parameters,
            Dictionary<string, (float min, float max)> conditionRanges) {
            
            var baseConfig = CreateBaseConfig(parameters);
            return new ConfigurableVitalSign(
                new VitalRangeConfig(parameters.Name, parameters.Unit, baseConfig.AgeRanges, conditionRanges)
            );
        }
        
        /// <summary>
        /// Creates a basic vital range configuration with age-based ranges
        /// </summary>
        private static VitalRangeConfig CreateBaseConfig(VitalSignParameters parameters) {
            var ageRanges = new AgeBasedRanges(
                (parameters.ChildMin, parameters.ChildMax),
                (parameters.AdultMin, parameters.AdultMax),
                (parameters.ElderlyMin, parameters.ElderlyMax)
            );
            
            return new VitalRangeConfig(parameters.Name, parameters.Unit, ageRanges);
        }
    }
}
