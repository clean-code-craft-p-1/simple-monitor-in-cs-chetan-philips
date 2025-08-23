namespace HealthMonitor.Infrastructure {
    /// <summary>
    /// Interface for alerting systems when vital signs are out of range.
    /// Supports different alert mechanisms (console, email, SMS, etc.).
    /// </summary>
    public interface IVitalSignAlerter {
        /// <summary>
        /// Sends an alert for an out-of-range vital sign.
        /// </summary>
        /// <param name="vitalName">Name of the vital sign that is out of range</param>
        /// <param name="value">The actual value that triggered the alert</param>
        /// <param name="unit">Unit of measurement for the vital sign</param>
        void Alert(string vitalName, float value, string unit);
    }
}