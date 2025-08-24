using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Pulse rate vital sign checker with age-based adjustments.
    /// </summary>
    public class PulseRate : StandardAgeBasedVitalSign {
        public PulseRate() : base(
            "Pulse Rate",
            "BPM",
            (VitalRangeConstants.PULSE_MIN_CHILD, VitalRangeConstants.PULSE_MAX_CHILD),
            (VitalRangeConstants.PULSE_MIN_ELDERLY, VitalRangeConstants.PULSE_MAX_ELDERLY),
            (VitalRangeConstants.PULSE_MIN_ADULT, VitalRangeConstants.PULSE_MAX_ADULT)
        ) { }
    }
}