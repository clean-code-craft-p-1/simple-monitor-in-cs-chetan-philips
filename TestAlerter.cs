using HealthMonitor;

/// <summary>
/// Test implementation of alerter that doesn't do I/O operations
/// </summary>
public class TestAlerter : VitalsAlerter
{
    /// <summary>
    /// Override to avoid console output during testing
    /// </summary>
    /// <param name="reading">The vital reading to check</param>
    public override void Alert(VitalReading reading) 
    {
        // Do nothing - avoids console output during testing
    }
}
