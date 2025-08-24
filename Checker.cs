using System;

using HealthMonitor.Core;

namespace HealthMonitor {
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
            var normalVitals = new VitalReading(98.6f, 72f, 95f, 120f, 80f);
            checker.CheckVitals(normalVitals);

            Console.WriteLine("\nTesting Abnormal Vitals:");
            var abnormalVitals = new VitalReading(104f, 110f, 85f, 160f, 100f);
            checker.CheckVitals(abnormalVitals);

            Console.WriteLine("\nDemonstrating Extensibility:");
            checker.RegisterVitalSign(new Tests.RespiratoryRate());

            var extendedVitals = new VitalReading();
            extendedVitals.SetReading("Temperature", 98.6f);
            extendedVitals.SetReading("Respiratory Rate", 25f); // High
            checker.CheckVitals(extendedVitals);
        }
    }
}