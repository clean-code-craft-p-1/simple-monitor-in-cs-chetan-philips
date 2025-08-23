namespace HealthMonitor.Models {
    /// <summary>
    /// Represents patient characteristics that can influence vital sign ranges
    /// </summary>
    public class PatientProfile {
        /// <summary>
        /// Gets or sets the age of the patient in years
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// Gets or sets the name of the patient
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets any additional patient-specific factors
        /// </summary>
        public string MedicalConditions { get; set; }
    }
}
