using HealthMonitor.Core;
using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Implementation of blood pressure vital sign checking.
    /// Supports age-based range adjustments for different patient profiles.
    /// </summary>
    public class BloodPressure : IVitalSign {
        private const float ADULT_MIN_SYSTOLIC = 90.0f;
        private const float ADULT_MAX_SYSTOLIC = 140.0f;
        private const float ADULT_MIN_DIASTOLIC = 60.0f;
        private const float ADULT_MAX_DIASTOLIC = 90.0f;
        
        private const float CHILD_MIN_SYSTOLIC = 80.0f;
        private const float CHILD_MAX_SYSTOLIC = 120.0f;
        private const float CHILD_MIN_DIASTOLIC = 50.0f;
        private const float CHILD_MAX_DIASTOLIC = 80.0f;
        
        private const float ELDERLY_MIN_SYSTOLIC = 100.0f;
        private const float ELDERLY_MAX_SYSTOLIC = 150.0f;
        private const float ELDERLY_MIN_DIASTOLIC = 65.0f;
        private const float ELDERLY_MAX_DIASTOLIC = 95.0f;

        public string Name => "Blood Pressure";
        public string Unit => "mmHg";

        public bool IsWithinRange(float value, PatientProfile profile = null) {
            var (minSystolic, maxSystolic, minDiastolic, maxDiastolic) = GetBloodPressureRange(profile);
            
            return (value >= minSystolic && value <= maxSystolic) || 
                   (value >= minDiastolic && value <= maxDiastolic);
        }

        public bool IsWithinRange(float systolic, float diastolic, PatientProfile profile = null) {
            var (minSystolic, maxSystolic, minDiastolic, maxDiastolic) = GetBloodPressureRange(profile);
            
            return systolic >= minSystolic && systolic <= maxSystolic &&
                   diastolic >= minDiastolic && diastolic <= maxDiastolic;
        }

        private (float minSystolic, float maxSystolic, float minDiastolic, float maxDiastolic) GetBloodPressureRange(PatientProfile profile) {
            if (profile?.Age == null) {
                return GetAdultBloodPressureRange();
            }

            return GetAgeSpecificBloodPressureRange(profile.Age.Value);
        }

        private (float, float, float, float) GetAgeSpecificBloodPressureRange(int age) {
            if (AgeClassifier.IsChild(age)) {
                return GetChildBloodPressureRange();
            }

            if (AgeClassifier.IsElderly(age)) {
                return GetElderlyBloodPressureRange();
            }

            return GetAdultBloodPressureRange();
        }

        private static (float, float, float, float) GetAdultBloodPressureRange() {
            return (ADULT_MIN_SYSTOLIC, ADULT_MAX_SYSTOLIC, ADULT_MIN_DIASTOLIC, ADULT_MAX_DIASTOLIC);
        }

        private static (float, float, float, float) GetChildBloodPressureRange() {
            return (CHILD_MIN_SYSTOLIC, CHILD_MAX_SYSTOLIC, CHILD_MIN_DIASTOLIC, CHILD_MAX_DIASTOLIC);
        }

        private static (float, float, float, float) GetElderlyBloodPressureRange() {
            return (ELDERLY_MIN_SYSTOLIC, ELDERLY_MAX_SYSTOLIC, ELDERLY_MIN_DIASTOLIC, ELDERLY_MAX_DIASTOLIC);
        }
    }
}