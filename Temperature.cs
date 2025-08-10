using System;

namespace HealthMonitor
{
    /// <summary>
    /// Implementation of temperature vital sign monitoring
    /// </summary>
    public class Temperature : IVitalSign
    {
        // Standard temperature ranges in Fahrenheit
        private const float StandardMinimum = 95.0f;
        private const float StandardMaximum = 102.0f;
        
        // Child-specific adjustments (children typically have higher normal temperatures)
        private const float ChildMinimumAdjustment = 0.5f;
        private const float ChildMaximumAdjustment = 0.5f;
        
        // Elderly-specific adjustments (elderly may have lower normal temperatures)
        private const float ElderlyMinimumAdjustment = -0.5f;
        private const float ElderlyMaximumAdjustment = -0.3f;

        /// <summary>Gets the name of this vital sign</summary>
        public string Name => "Temperature";
        
        /// <summary>Gets the unit of measurement</summary>
        public string Unit => "°F";
        
        /// <summary>Gets vendor-specific information</summary>
        public string VendorInfo => "Standard Medical Thermometer";

        /// <summary>Checks if temperature is within normal range (95-102 °F)</summary>
        /// <param name="value">The temperature value in Fahrenheit</param>
        /// <returns>True if temperature is within normal range</returns>
        public bool IsWithinRange(float value) => value >= StandardMinimum && value <= StandardMaximum;
        
        /// <summary>Checks if temperature is within range based on patient profile</summary>
        /// <param name="value">The temperature value in Fahrenheit</param>
        /// <param name="patientProfile">The patient's profile</param>
        /// <returns>True if temperature is within patient-specific range</returns>
        public bool IsWithinRange(float value, PatientProfile patientProfile)
        {
            var (min, max) = GetRange(patientProfile);
            return value >= min && value <= max;
        }
        
        /// <summary>Gets the normal temperature range</summary>
        /// <returns>Minimum and maximum normal temperature values</returns>
        public (float Min, float Max) GetRange() => (StandardMinimum, StandardMaximum);
        
        /// <summary>Gets patient-specific temperature range</summary>
        /// <param name="patientProfile">The patient's profile</param>
        /// <returns>Patient-specific minimum and maximum normal temperature values</returns>
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
            
            return (StandardMinimum + minAdjustment, StandardMaximum + maxAdjustment);
        }
    }
}
