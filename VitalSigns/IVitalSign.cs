using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns
{
    /// <summary>
    /// Interface for vital sign implementations.
    /// </summary>
    public interface IVitalSign
    {
        string Name { get; }
        string Unit { get; }
        bool IsWithinRange(float value, PatientProfile profile = null);
    }
}