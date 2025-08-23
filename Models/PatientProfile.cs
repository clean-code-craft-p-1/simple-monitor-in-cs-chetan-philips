namespace HealthMonitor.Models {
    /// <summary>
    /// Represents patient-specific information for personalized vital sign checking.
    /// Contains demographic and medical information that affects normal ranges.
    /// </summary>
    public class PatientProfile {
        /// <summary>
        /// Gets or sets the patient's age in years.
        /// Used for age-based vital sign range adjustments.
        /// </summary>
        public int? Age { get; set; }

        /// <summary>
        /// Gets or sets the patient's full name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets any relevant medical conditions.
        /// May affect normal vital sign ranges.
        /// </summary>
        public string MedicalConditions { get; set; }
    }
}