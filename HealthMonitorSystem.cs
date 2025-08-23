using System;
using HealthMonitor.Core;
using HealthMonitor.Models;

namespace HealthMonitor {
    public static class HealthMonitorSystem {
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
            var checker = new VitalsChecker(new VitalSignAlerter());

            Console.WriteLine("\nTesting Normal Vitals:");
            var normalVitals = new VitalReading(98.6f, 72f, 95f, 120f, 80f);
            checker.CheckVitals(normalVitals);
            Console.WriteLine("Normal vitals checked - no alerts expected");

            Console.WriteLine("\nTesting Abnormal Vitals:");
            var abnormalVitals = new VitalReading(104f, 110f, 85f, 160f, 100f);
            checker.CheckVitals(abnormalVitals);

            Console.WriteLine("\nDemonstrating Runtime Extensibility:");
            checker.RegisterVitalSign(new Tests.RespiratoryRate());

            var extendedVitals = new VitalReading();
            extendedVitals.SetReading("Respiratory Rate", 25f); // High
            checker.CheckVitals(extendedVitals);
        }
    }

    // Example of how easy it is to add new vital signs
    public class RespiratoryRate : IVitalSign {
        public string Name => "Respiratory Rate";
        public string Unit => "breaths/min";

        public bool IsWithinRange(float value, PatientProfile profile = null) {
            var (min, max) = profile?.Age switch {
                < 12 => (20f, 30f),     // Child
                >= 65 => (12f, 28f),    // Elderly  
                _ => (12f, 20f)         // Adult
            };
            return value >= min && value <= max;
        }
    }
}