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
            var normalVitals = VitalReadingFactory.CreateNormalVitals();
            checker.CheckVitals(normalVitals);

            Console.WriteLine("\nTesting Abnormal Vitals:");
            var abnormalVitals = VitalReadingFactory.CreateAbnormalVitals();
            checker.CheckVitals(abnormalVitals);

            Console.WriteLine("\nDemonstrating Extensibility:");
            // Use the factory to create a respiratory rate vital sign
            checker.RegisterVitalSign(VitalSignFactory.CreateRespiratoryRate());

            var extendedVitals = VitalReadingFactory.CreateRespiratoryRateReading();
            checker.CheckVitals(extendedVitals);
        }
    }
}