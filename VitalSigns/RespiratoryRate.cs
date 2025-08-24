using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns 
{
    /// <summary>
    /// Respiratory rate vital sign for monitoring breathing frequency.
    /// </summary>
    public class RespiratoryRate : AgeBasedVitalSign 
    {
        public override string Name => "Respiratory Rate";
        public override string Unit => "breaths/min";

        protected override (float min, float max) GetAgeSpecificRange(int? age) {
            return AgeBasedRangeHelper.GetRangeByAge(age,
                (VitalRangeConstants.RESP_MIN_CHILD, VitalRangeConstants.RESP_MAX_CHILD),
                (VitalRangeConstants.RESP_MIN_ELDERLY, VitalRangeConstants.RESP_MAX_ELDERLY),
                (VitalRangeConstants.RESP_MIN_ADULT, VitalRangeConstants.RESP_MAX_ADULT)
            );
        }
    }
}
