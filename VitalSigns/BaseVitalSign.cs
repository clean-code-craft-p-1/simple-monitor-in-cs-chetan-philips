using HealthMonitor.Models;

namespace HealthMonitor.VitalSigns
{
    /// <summary>
    /// Common base class for all vital sign implementations to eliminate duplication.
    /// </summary>
    public abstract class BaseVitalSign : IVitalSign 
    {
        // Protected constructor to prevent instantiation except by derived classes
        protected BaseVitalSign() { }
        
        public abstract string Name { get; }
        public abstract string Unit { get; }
        
        public abstract bool IsWithinRange(float value, PatientProfile profile = null);
    }
}
