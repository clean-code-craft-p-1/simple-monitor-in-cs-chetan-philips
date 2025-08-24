namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Constants for vital sign ranges and age thresholds
    /// </summary>
    public static class VitalRangeConstants {
        // Age thresholds
        public const int CHILD_AGE_THRESHOLD = 12;
        public const int CHILD_MAX_AGE = CHILD_AGE_THRESHOLD;
        public const int ELDERLY_AGE_THRESHOLD = 65;
        public const int ELDERLY_MIN_AGE = ELDERLY_AGE_THRESHOLD;

        // Temperature ranges (Fahrenheit)
        public const float TEMP_MIN_CHILD = 95f;
        public const float TEMP_MAX_CHILD = 103f;
        public const float TEMP_MIN_ELDERLY = 94f;
        public const float TEMP_MAX_ELDERLY = 102f;
        public const float TEMP_MIN_ADULT = 95f;
        public const float TEMP_MAX_ADULT = 102f;
        public const float TEMP_NORMAL = 98.6f;
        public const float TEMP_HIGH = 104f;

        // Pulse rate ranges (BPM)
        public const float PULSE_MIN_CHILD = 70f;
        public const float PULSE_MAX_CHILD = 120f;
        public const float PULSE_MIN_ELDERLY = 55f;
        public const float PULSE_MAX_ELDERLY = 105f;
        public const float PULSE_MIN_ADULT = 60f;
        public const float PULSE_MAX_ADULT = 100f;
        public const float PULSE_NORMAL = 72f;
        public const float PULSE_HIGH = 110f;

        // Oxygen saturation ranges (%)
        public const float OXY_MIN = 90f;
        public const float OXY_MAX = 100f;
        public const float OXY_NORMAL = 95f;
        public const float OXY_LOW = 85f;
        public const float OXY_COPD = 87f;

        // Systolic blood pressure ranges (mmHg)
        public const float SYS_MIN_CHILD = 80f;
        public const float SYS_MAX_CHILD = 120f;
        public const float SYS_MIN_ELDERLY = 110f;
        public const float SYS_MAX_ELDERLY = 150f;
        public const float SYS_MIN_ADULT = 110f;
        public const float SYS_MAX_ADULT = 140f;
        public const float SYS_NORMAL = 120f;
        public const float SYS_HIGH = 160f;

        // Diastolic blood pressure ranges (mmHg)
        public const float DIA_MIN_CHILD = 50f;
        public const float DIA_MAX_CHILD = 80f;
        public const float DIA_MIN_ELDERLY = 65f;
        public const float DIA_MAX_ELDERLY = 95f;
        public const float DIA_MIN_ADULT = 60f;
        public const float DIA_MAX_ADULT = 90f;
        public const float DIA_NORMAL = 80f;
        public const float DIA_HIGH = 100f;

        // Respiratory rate ranges (breaths/min)
        public const float RESP_MIN_CHILD = 20f;
        public const float RESP_MAX_CHILD = 30f;
        public const float RESP_MIN_ELDERLY = 12f;
        public const float RESP_MAX_ELDERLY = 28f;
        public const float RESP_MIN_ADULT = 12f;
        public const float RESP_MAX_ADULT = 20f;
        public const float RESP_HIGH = 25f;
    }
}
