using System.Collections.Generic;

using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Configuration class for vital sign ranges that supports both age-based and condition-based ranges.
    /// This centralizes range configurations to reduce duplication and lower cyclomatic complexity.
    /// </summary>
    public class VitalRangeConfig {
        /// <summary>
        /// Gets the name of the vital sign
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the unit of measurement for the vital sign
        /// </summary>
        public string Unit { get; }

        /// <summary>
        /// Gets the age-specific ranges for this vital sign
        /// </summary>
        public AgeBasedRanges AgeRanges { get; }

        /// <summary>
        /// Gets the condition-specific ranges for this vital sign
        /// </summary>
        public IReadOnlyDictionary<string, (float min, float max)> ConditionRanges { get; protected set; }

        /// <summary>
        /// Determines whether this vital sign uses condition-based range adjustments
        /// </summary>
        public bool IsConditionBased => ConditionRanges?.Count > 0;

        /// <summary>
        /// Creates a new age-based vital sign configuration
        /// </summary>
        /// <param name="name">The name of the vital sign</param>
        /// <param name="unit">The unit of measurement</param>
        /// <param name="ageRanges">Age-specific ranges</param>
        public VitalRangeConfig(string name, string unit, AgeBasedRanges ageRanges) {
            Name = name;
            Unit = unit;
            AgeRanges = ageRanges;
            ConditionRanges = new Dictionary<string, (float min, float max)>();
        }

        /// <summary>
        /// Creates a new vital sign configuration with both age and condition-based ranges
        /// </summary>
        /// <param name="name">The name of the vital sign</param>
        /// <param name="unit">The unit of measurement</param>
        /// <param name="ageRanges">Age-specific ranges</param>
        /// <param name="conditionRanges">Condition-specific ranges</param>
        public VitalRangeConfig(string name, string unit, AgeBasedRanges ageRanges,
            IReadOnlyDictionary<string, (float min, float max)> conditionRanges) {
            Name = name;
            Unit = unit;
            AgeRanges = ageRanges;
            ConditionRanges = conditionRanges ?? new Dictionary<string, (float min, float max)>();
        }

        /// <summary>
        /// Gets the appropriate range for the given profile, considering both age and medical conditions
        /// </summary>
        /// <param name="profile">The patient profile</param>
        /// <returns>A tuple containing the minimum and maximum acceptable values</returns>
        public (float min, float max) GetRange(PatientProfile profile) {
            // Try to get condition-specific range if applicable
            var conditionRange = TryGetConditionRange(profile);
            if (conditionRange.HasValue) {
                return conditionRange.Value;
            }

            // Fall back to age-based ranges
            return AgeRanges.GetRangeForAge(profile?.Age);
        }

        /// <summary>
        /// Tries to get a condition-specific range for the patient profile
        /// </summary>
        /// <param name="profile">The patient profile</param>
        /// <returns>The condition-specific range if found; otherwise null</returns>
        private (float min, float max)? TryGetConditionRange(PatientProfile profile) {
            if (!HasValidConditions(profile)) {
                return null;
            }

            return FindMatchingConditionRange(profile.MedicalConditions);
        }

        /// <summary>
        /// Checks if the profile has valid medical conditions and if this vital sign uses condition-based ranges
        /// </summary>
        /// <param name="profile">The patient profile to check</param>
        /// <returns>True if the profile has valid conditions; otherwise false</returns>
        private bool HasValidConditions(PatientProfile profile) {
            return profile?.MedicalConditions != null && IsConditionBased;
        }

        /// <summary>
        /// Finds a matching condition range from the patient's medical conditions
        /// </summary>
        /// <param name="medicalConditions">List of patient's medical conditions</param>
        /// <returns>The range if a match is found; otherwise null</returns>
        private (float min, float max)? FindMatchingConditionRange(IEnumerable<string> medicalConditions) {
            foreach (var condition in medicalConditions) {
                if (ConditionRanges.TryGetValue(condition, out var range)) {
                    return range;
                }
            }

            return null;
        }
    }
}
