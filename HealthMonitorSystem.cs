using System;

using HealthMonitor.Core;
using HealthMonitor.Infrastructure;
using HealthMonitor.Models;

namespace HealthMonitor {
    /// <summary>
    /// Main entry point for the Health Monitor System.
    /// Demonstrates vital sign checking with various scenarios.
    /// Maintains backward compatibility with original interface.
    /// </summary>
    public static class HealthMonitorSystem {
        /// <summary>
        /// Application entry point - demonstrates the health monitoring system.
        /// Runs tests first, then demonstrates normal and abnormal vital scenarios.
        /// </summary>
        public static void Main() {
            Console.WriteLine("Health Monitor System Starting...");
            Console.WriteLine();

            // Run comprehensive tests first
            Console.WriteLine("Running system tests...");
            try {
                Tests.CheckerTests.RunAllTests();
                Console.WriteLine("All tests passed!");
            } catch (Exception ex) {
                Console.WriteLine($"Tests failed: {ex.Message}");
                return;
            }
            Console.WriteLine();

            // Initialize system components
            var alerter = new VitalSignAlerter();
            var checker = new VitalsChecker(alerter);

            // Demonstrate normal vitals
            Console.WriteLine("Testing Normal Vitals...");
            var normalVitals = new VitalReading(98.6f, 72, 95);
            checker.CheckVitals(normalVitals);
            if (checker.AreAllVitalsWithinRange(normalVitals)) {
                Console.WriteLine("All vitals are within normal range");
            }
            Console.WriteLine();

            // Demonstrate abnormal vitals
            Console.WriteLine("Testing Abnormal Vitals...");
            var abnormalVitals = new VitalReading(104.0f, 110, 85);
            checker.CheckVitals(abnormalVitals);
            Console.WriteLine();

            // Demonstrate patient-specific checking
            Console.WriteLine("Testing with Patient Profile...");
            var elderlyPatient = new PatientProfile {
                Age = 70,
                Name = "John Smith",
                MedicalConditions = "Hypertension"
            };

            var elderlyVitals = new VitalReading(94.5f, 75, 92);
            Console.WriteLine($"Checking vitals for {elderlyPatient.Name} (Age: {elderlyPatient.Age})");
            checker.CheckVitals(elderlyVitals, elderlyPatient);

            if (checker.AreAllVitalsWithinRange(elderlyVitals, elderlyPatient)) {
                Console.WriteLine("All vitals are within normal range for this patient profile");
            }

            Console.WriteLine();
            Console.WriteLine("Health Monitor System Complete.");
        }
    }
}