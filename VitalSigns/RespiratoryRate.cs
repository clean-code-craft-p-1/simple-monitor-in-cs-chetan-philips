using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns
{
    /// <summary>
    /// Respiratory rate vital sign for monitoring breathing frequency.
    /// </summary>
    public class RespiratoryRate : StandardAgeBasedVitalSign
    {
        public RespiratoryRate() : base(
            "Respiratory Rate",
            "breaths/min",
            (VitalRangeConstants.RESP_MIN_CHILD, VitalRangeConstants.RESP_MAX_CHILD),
            (VitalRangeConstants.RESP_MIN_ELDERLY, VitalRangeConstants.RESP_MAX_ELDERLY),
            (VitalRangeConstants.RESP_MIN_ADULT, VitalRangeConstants.RESP_MAX_ADULT)
        ) { }
    }
}
