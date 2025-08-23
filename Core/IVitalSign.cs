using HealthMonitor.Models;

namespace HealthMonitor.Core {
    /// <summary>
    /// Interface for vital sign implementations.
    /// Defines the contract for checking if vital sign values are within normal ranges.
    /// </summary>
    public interface IVitalSign {
        /// <summary>
        /// Gets the name of this vital sign.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the unit of measurement for this vital sign.
        /// </summary>
        string Unit { get; }

        /// <summary>
        /// Determines if the vital sign value is within normal range.
        /// </summary>
        /// <param name="value">The vital sign value to check</param>
        /// <param name="profile">Optional patient profile for personalized ranges</param>
        /// <returns>True if the value is within normal range</returns>
        bool IsWithinRange(float value, PatientProfile profile = null);
    }
}