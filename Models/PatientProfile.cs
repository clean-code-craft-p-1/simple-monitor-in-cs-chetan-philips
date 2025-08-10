using System;

namespace HealthMonitor.Models {
    /// <summary>
    /// Represents patient characteristics that can influence vital sign ranges
    /// </summary>
    public class PatientProfile {
        // Constants for age thresholds to avoid magic numbers
        public const int ChildAgeThreshold = 18;
        public const int ElderlyAgeThreshold = 65;

        /// <summary>Gets or sets the age of the patient in years</summary>
        public int Age { get; set; }

        /// <summary>Gets or sets whether the patient is a child (under 18)</summary>
        public bool IsChild => Age < ChildAgeThreshold;

        /// <summary>Gets or sets whether the patient is elderly (over 65)</summary>
        public bool IsElderly => Age > ElderlyAgeThreshold;

        /// <summary>Gets or sets any additional patient-specific factors</summary>
        public string[] Conditions { get; set; } = Array.Empty<string>();
    }
}
