namespace HealthMonitor.Core {
    /// <summary>
    /// Interface for vital sign alerting functionality.
    /// </summary>
    public interface IVitalSignAlerter {
        void Alert(string vitalName, string value, string unit);
    }
}