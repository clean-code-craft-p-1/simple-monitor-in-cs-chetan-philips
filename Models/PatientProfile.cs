namespace HealthMonitor.Models {
    /// <summary>
    /// Represents a patient profile with demographic and medical information.
    /// Used for personalized vital sign range adjustments.
    /// </summary>
    public class PatientProfile {
        /// <summary>
        /// Gets or sets the patient's age in years.
        /// Used for age-based vital sign range adjustments.
        /// </summary>
        public int? Age { get; set; }

        /// <summary>
        /// Gets or sets the patient's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the patient's medical conditions.
        /// Used for condition-specific vital sign adjustments.
        /// </summary>
        public string MedicalConditions { get; set; }
    }
}