using System;

namespace HealthMonitor
{
    /// <summary>
    /// Implementation of oxygen saturation vital sign monitoring
    /// </summary>
    public class OxygenSaturation : IVitalSign
    {
        // Standard oxygen saturation ranges in percentage
        private const float StandardMinimum = 90.0f;
        private const float StandardMaximum = 100.0f;
        
        // Child-specific adjustments (children typically have similar SpO2 requirements)
        private const float ChildMinimumAdjustment = 0.0f;
        private const float ChildMaximumAdjustment = 0.0f;
        
        // Elderly-specific adjustments (elderly may have slightly lower tolerance)
        private const float ElderlyMinimumAdjustment = -2.0f;
        private const float ElderlyMaximumAdjustment = 0.0f;
        
        // COPD patient adjustments (patients with chronic conditions may have different norms)
        private const float CopdMinimumAdjustment = -4.0f;

        /// <summary>Gets the name of this vital sign</summary>
        public string Name => "Oxygen Saturation";
        
        /// <summary>Gets the unit of measurement</summary>
        public string Unit => "%";
        
        /// <summary>Gets vendor-specific information</summary>
        public string VendorInfo => "Standard Pulse Oximeter";

        /// <summary>Checks if SPO2 is within normal range (90-100%)</summary>
        /// <param name="value">The oxygen saturation percentage</param>
        /// <returns>True if SPO2 is within normal range</returns>
        public bool IsWithinRange(float value) => value >= StandardMinimum && value <= StandardMaximum;
        
        /// <summary>Checks if SPO2 is within range based on patient profile</summary>
        /// <param name="value">The oxygen saturation percentage</param>
        /// <param name="patientProfile">The patient's profile</param>
        /// <returns>True if SPO2 is within patient-specific range</returns>
        public bool IsWithinRange(float value, PatientProfile patientProfile)
        {
            var (min, max) = GetRange(patientProfile);
            return value >= min && value <= max;
        }
        
        /// <summary>Gets the normal SPO2 range</summary>
        /// <returns>Minimum and maximum normal SPO2 values</returns>
        public (float Min, float Max) GetRange() => (StandardMinimum, StandardMaximum);
        
        /// <summary>Gets patient-specific SPO2 range</summary>
        /// <param name="patientProfile">The patient's profile</param>
        /// <returns>Patient-specific minimum and maximum normal SPO2 values</returns>
        public (float Min, float Max) GetRange(PatientProfile patientProfile)
        {
            float minAdjustment = 0;
            float maxAdjustment = 0;
            
            if (patientProfile.IsChild)
            {
                minAdjustment += ChildMinimumAdjustment;
                maxAdjustment += ChildMaximumAdjustment;
            }
            
            if (patientProfile.IsElderly)
            {
                minAdjustment += ElderlyMinimumAdjustment;
                maxAdjustment += ElderlyMaximumAdjustment;
            }
            
            // Check for COPD in patient conditions
            if (patientProfile.Conditions != null && 
                Array.Exists(patientProfile.Conditions, condition => 
                    condition.Equals("COPD", StringComparison.OrdinalIgnoreCase)))
            {
                minAdjustment += CopdMinimumAdjustment;
            }
            
            return (StandardMinimum + minAdjustment, StandardMaximum + maxAdjustment);
        }
    }
}
