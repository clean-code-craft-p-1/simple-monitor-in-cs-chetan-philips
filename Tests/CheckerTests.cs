using System.Collections.Generic;

using Xunit;

using HealthMonitor;
using HealthMonitor.Core;
using HealthMonitor.VitalSigns;

public class CheckerTests {
    // Legacy compatibility tests
    [Fact]
    public void NotOkWhenAnyVitalIsOffRange() {
        // Original test - pulseRate is out of range
        Assert.False(Checker.VitalsOk(99f, 102, 70));
        // Original test - all vitals within range
        Assert.True(Checker.VitalsOk(98.1f, 70, 98));
    }

    // Tests for temperature vital sign
    [Theory]
    [InlineData(95f, true)]   // Lower boundary
    [InlineData(94.9f, false)] // Just below lower boundary
    [InlineData(102f, true)]   // Upper boundary
    [InlineData(102.1f, false)] // Just above upper boundary
    [InlineData(98.6f, true)]   // Normal temperature
    public void TemperatureRangeChecks(float temp, bool expectedResult) {
        var temperature = new Temperature();
        Assert.Equal(expectedResult, temperature.IsWithinRange(temp));
    }

    // Tests for pulse rate vital sign
    [Theory]
    [InlineData(60, true)]   // Lower boundary
    [InlineData(59, false)]  // Just below lower boundary
    [InlineData(100, true)]  // Upper boundary
    [InlineData(101, false)] // Just above upper boundary
    [InlineData(75, true)]   // Normal pulse rate
    public void PulseRateRangeChecks(float pulse, bool expectedResult) {
        var pulseRate = new PulseRate();
        Assert.Equal(expectedResult, pulseRate.IsWithinRange(pulse));
    }

    // Tests for SpO2 vital sign
    [Theory]
    [InlineData(90, true)]   // Lower boundary
    [InlineData(89, false)]  // Just below lower boundary
    [InlineData(100, true)]  // Upper boundary
    [InlineData(98, true)]   // Normal SpO2
    public void SpO2RangeChecks(float spo2, bool expectedResult) {
        var oxygenSaturation = new OxygenSaturation();
        Assert.Equal(expectedResult, oxygenSaturation.IsWithinRange(spo2));
    }

    // Tests for VitalReading class
    [Fact]
    public void VitalReadingIdentifiesOutOfRangeValues() {
        var tempVital = new Temperature();
        var pulseVital = new PulseRate();
        var spo2Vital = new OxygenSaturation();

        var normalTempReading = new VitalReading(tempVital, 98.6f);
        var highTempReading = new VitalReading(tempVital, 103f);
        var lowPulseReading = new VitalReading(pulseVital, 50f);
        var normalSpo2Reading = new VitalReading(spo2Vital, 95f);

        Assert.True(normalTempReading.IsWithinRange);
        Assert.False(highTempReading.IsWithinRange);
        Assert.False(lowPulseReading.IsWithinRange);
        Assert.True(normalSpo2Reading.IsWithinRange);
    }

    // Test for VitalsChecker pure function
    [Fact]
    public void AreAllVitalsWithinRangeIdentifiesOutOfRangeReadings() {
        var checker = new VitalsChecker();
        var tempVital = new Temperature();
        var pulseVital = new PulseRate();
        var spo2Vital = new OxygenSaturation();

        // All vitals in range
        var allInRangeReadings = new List<VitalReading>
        {
            new VitalReading(tempVital, 98.6f),
            new VitalReading(pulseVital, 75f),
            new VitalReading(spo2Vital, 95f)
        };

        // One vital out of range
        var oneOutOfRangeReadings = new List<VitalReading>
        {
            new VitalReading(tempVital, 98.6f),
            new VitalReading(pulseVital, 110f), // Out of range pulse
            new VitalReading(spo2Vital, 95f)
        };

        Assert.True(checker.AreAllVitalsWithinRange(allInRangeReadings));
        Assert.False(checker.AreAllVitalsWithinRange(oneOutOfRangeReadings));
    }

    // Edge case tests
    [Fact]
    public void CheckVitalsHandlesAllCombinationsOfOutOfRangeVitals() {
        // Create a test implementation of VitalsChecker that doesn't do I/O
        var mockChecker = new TestVitalsChecker();

        // All vitals normal
        Assert.True(mockChecker.CheckVitals(98.6f, 75f, 95f));

        // Each vital out of range individually
        Assert.False(mockChecker.CheckVitals(94f, 75f, 95f));   // Low temp
        Assert.False(mockChecker.CheckVitals(103f, 75f, 95f));  // High temp
        Assert.False(mockChecker.CheckVitals(98.6f, 55f, 95f)); // Low pulse
        Assert.False(mockChecker.CheckVitals(98.6f, 110f, 95f)); // High pulse
        Assert.False(mockChecker.CheckVitals(98.6f, 75f, 85f));  // Low SpO2

        // Multiple vitals out of range
        Assert.False(mockChecker.CheckVitals(94f, 55f, 95f));     // Low temp, low pulse
        Assert.False(mockChecker.CheckVitals(103f, 75f, 85f));    // High temp, low SpO2
        Assert.False(mockChecker.CheckVitals(98.6f, 110f, 85f));  // High pulse, low SpO2
        Assert.False(mockChecker.CheckVitals(94f, 110f, 85f));    // All vitals out of range
    }
}