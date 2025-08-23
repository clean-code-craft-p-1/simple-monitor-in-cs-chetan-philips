namespace HealthMonitor.Infrastructure {
    /// <summary>
    /// Interface for vital sign alerting functionality.
    /// Allows different alerting implementations (console, file, network, etc.).
    /// </summary>
    public interface IVitalSignAlerter {
        /// <summary>
        /// Alerts about an abnormal vital sign reading.
        /// </summary>
        /// <param name="vitalName">Name of the vital sign</param>
        /// <param name="value">The abnormal value</param>
        /// <param name="unit">Unit of measurement</param>
        void Alert(string vitalName, string value, string unit);
    }
}