namespace HealthMonitor.Infrastructure {
    /// <summary>
    /// Interface for vital sign alerting mechanism
    /// </summary>
    public interface IVitalSignAlerter {
        void Alert(string message);
    }
}