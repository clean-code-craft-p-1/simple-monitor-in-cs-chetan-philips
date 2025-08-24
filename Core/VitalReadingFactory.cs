using HealthMonitor.Models;

namespace HealthMonitor.Core
{
    /// <summary>
    /// Factory for creating common vital reading combinations to reduce duplication.
    /// </summary>
    public static class VitalReadingFactory
    {
        public static VitalReading CreateNormalVitals()
        {
            return new VitalReading(
                VitalRangeConstants.TEMP_NORMAL,
                VitalRangeConstants.PULSE_NORMAL,
                VitalRangeConstants.OXY_NORMAL,
                VitalRangeConstants.SYS_NORMAL,
                VitalRangeConstants.DIA_NORMAL
            );
        }

        public static VitalReading CreateAbnormalVitals()
        {
            return new VitalReading(
                VitalRangeConstants.TEMP_HIGH,
                VitalRangeConstants.PULSE_HIGH,
                VitalRangeConstants.OXY_LOW,
                VitalRangeConstants.SYS_HIGH,
                VitalRangeConstants.DIA_HIGH
            );
        }

        public static VitalReading CreateRespiratoryRateReading(float value = VitalRangeConstants.RESP_HIGH)
        {
            var reading = new VitalReading();
            reading.SetReading("Respiratory Rate", value);
            reading.SetReading("Temperature", VitalRangeConstants.TEMP_NORMAL); // Add normal temp for context
            return reading;
        }
    }
}
