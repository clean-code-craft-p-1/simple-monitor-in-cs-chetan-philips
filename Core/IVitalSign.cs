using HealthMonitor.Models;

namespace HealthMonitor.Core {
    /// <summary>
    /// Interface for vital sign implementations that can check if values are within normal ranges.
    /// Supports patient-specific ranges based on profile information.
    /// </summary>
    public interface IVitalSign {
        /// <summary>
        /// Gets the name of the vital sign.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the unit of measurement for this vital sign.
        /// </summary>
        string Unit { get; }

        /// <summary>
        /// Determines if the given value is within the normal range for this vital sign.
        /// </summary>
        /// <param name="value">The vital sign value to check</param>
        /// <param name="profile">Optional patient profile for personalized ranges</param>
        /// <returns>True if the value is within normal range, false otherwise</returns>
        bool IsWithinRange(float value, PatientProfile profile = null);
    }
}