using HealthMonitor.Models;

namespace HealthMonitor.Core {
    /// <summary>
    /// Interface for vital signs to support future extension
    /// </summary>
    public interface IVitalSign {
        /// <summary>Gets the name of the vital sign</summary>
        string Name { get; }

        /// <summary>Gets the unit of measurement for this vital sign</summary>
        string Unit { get; }

        /// <summary>Gets vendor-specific information if available</summary>
        string VendorInfo { get; }

        /// <summary>Checks if the given value is within the normal range for this vital sign</summary>
        /// <param name="value">The vital sign value to check</param>
        /// <returns>True if the value is within normal range, false otherwise</returns>
        bool IsWithinRange(float value);

        /// <summary>Checks if the given value is within normal range for a specific patient</summary>
        /// <param name="value">The vital sign value to check</param>
        /// <param name="patientProfile">The patient's characteristics</param>
        /// <returns>True if the value is within normal range, false otherwise</returns>
        bool IsWithinRange(float value, PatientProfile patientProfile);

        /// <summary>Gets the minimum and maximum normal range values for this vital sign</summary>
        /// <returns>A tuple containing the minimum and maximum normal values</returns>
        (float Min, float Max) GetRange();

        /// <summary>Gets the range adjusted for a specific patient</summary>
        /// <param name="patientProfile">The patient's characteristics</param>
        /// <returns>A tuple containing the patient-specific minimum and maximum normal values</returns>
        (float Min, float Max) GetRange(PatientProfile patientProfile);
    }
}
