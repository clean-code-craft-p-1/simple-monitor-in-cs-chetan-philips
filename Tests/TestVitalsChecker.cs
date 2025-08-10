using System.Collections.Generic;

using HealthMonitor.Core;
using HealthMonitor.VitalSigns;

/// <summary>
/// Test implementation of VitalsChecker that doesn't do I/O operations
/// </summary>
public class TestVitalsChecker : VitalsChecker {
    /// <summary>
    /// Constructor that initializes with test implementations
    /// </summary>
    public TestVitalsChecker() : base(new TestAlerter(), new Temperature(), new PulseRate(), new OxygenSaturation()) {
    }

    /// <summary>
    /// Override to avoid console output during testing
    /// </summary>
    /// <param name="temperature">Temperature in Fahrenheit</param>
    /// <param name="pulseRate">Pulse rate in beats per minute</param>
    /// <param name="spo2">Oxygen saturation percentage</param>
    /// <returns>True if all vitals are within normal range</returns>
    public override bool CheckVitals(float temperature, float pulseRate, float spo2) {
        var readings = new List<VitalReading>
        {
            new VitalReading(new Temperature(), temperature),
            new VitalReading(new PulseRate(), pulseRate),
            new VitalReading(new OxygenSaturation(), spo2)
        };

        return AreAllVitalsWithinRange(readings);
    }
}
