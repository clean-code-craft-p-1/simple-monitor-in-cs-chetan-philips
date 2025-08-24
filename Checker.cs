using System;

using HealthMonitor.Core;
using HealthMonitor.VitalSigns;

namespace HealthMonitor {
    /// <summary>
    /// Main entry point for the Health Monitor System.
    /// Demonstrates vital sign monitoring functionality and runs tests.
    /// </summary>
    public static class Checker {
        public static void Main() {
            Console.WriteLine("Health Monitor System Starting...");

            RunTests();
            DemonstrateSystem();

            Console.WriteLine("Health Monitor System Complete.");
        }

        private static void RunTests() {
            Console.WriteLine("Running tests...");
            try {
                Tests.CheckerTests.RunAllTests();
                Console.WriteLine("All tests passed!");
            } catch (Exception ex) {
                Console.WriteLine($"Tests failed: {ex.Message}");
            }
        }

        private static void DemonstrateSystem() {
            var checker = new VitalsChecker();

            Console.WriteLine("\nTesting Normal Vitals:");
            var normalVitals = new VitalReading(
                VitalRangeConstants.TEMP_NORMAL, 
                VitalRangeConstants.PULSE_NORMAL, 
                VitalRangeConstants.OXY_NORMAL, 
                VitalRangeConstants.SYS_NORMAL, 
                VitalRangeConstants.DIA_NORMAL
            );
            checker.CheckVitals(normalVitals);

            Console.WriteLine("\nTesting Abnormal Vitals:");
            var abnormalVitals = new VitalReading(
                VitalRangeConstants.TEMP_HIGH, 
                VitalRangeConstants.PULSE_HIGH, 
                VitalRangeConstants.OXY_LOW, 
                VitalRangeConstants.SYS_HIGH, 
                VitalRangeConstants.DIA_HIGH
            );
            checker.CheckVitals(abnormalVitals);

            Console.WriteLine("\nDemonstrating Extensibility:");
            checker.RegisterVitalSign(new VitalSigns.RespiratoryRate());

            var extendedVitals = new VitalReading();
            extendedVitals.SetReading("Temperature", VitalRangeConstants.TEMP_NORMAL);
            extendedVitals.SetReading("Respiratory Rate", VitalRangeConstants.RESP_HIGH); // High
            checker.CheckVitals(extendedVitals);
        }
    }
}