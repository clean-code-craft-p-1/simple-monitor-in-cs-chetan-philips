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

            if (!RunSystemTests()) {
                return;
            }

            DemonstrateVitalSignScenarios();

            Console.WriteLine("Health Monitor System Complete.");
        }

        private static bool RunSystemTests() {
            // Run comprehensive tests first
            Console.WriteLine("Running system tests...");
            try {
                Tests.CheckerTests.RunAllTests();
                Console.WriteLine("All tests passed!");
                return true;
            } catch (Exception ex) {
                Console.WriteLine($"Tests failed: {ex.Message}");
                return false;
            }
        }

        private static void DemonstrateVitalSignScenarios() {
            // Initialize system components
            var checker = CreateVitalsChecker();

            // Demonstrate normal and abnormal scenarios
            DemonstrateNormalVitals(checker);
            DemonstrateAbnormalVitals(checker);
            DemonstratePatientSpecificChecking(checker);
        }

        private static VitalsChecker CreateVitalsChecker() {
            var alerter = new VitalSignAlerter();
            return new VitalsChecker(alerter);
        }

        private static void DemonstrateNormalVitals(VitalsChecker checker) {
            Console.WriteLine("Testing Normal Vitals...");
            var normalVitals = new VitalReading(98.6f, 72, 95, 120f, 80f);

            checker.CheckVitals(normalVitals);
            CheckAndReportVitalStatus(checker, normalVitals);
        }

        private static void DemonstrateAbnormalVitals(VitalsChecker checker) {
            Console.WriteLine("Testing Abnormal Vitals...");
            var abnormalVitals = new VitalReading(104.0f, 110, 85, 160f, 100f);
            checker.CheckVitals(abnormalVitals);
        }

        private static void DemonstratePatientSpecificChecking(VitalsChecker checker) {
            Console.WriteLine("Testing with Patient Profile...");
            var elderlyPatient = CreateElderlyPatient();
            var elderlyVitals = new VitalReading(94.5f, 75, 92, 145f, 92f);

            Console.WriteLine($"Checking vitals for {elderlyPatient.Name} (Age: {elderlyPatient.Age})");
            checker.CheckVitals(elderlyVitals, elderlyPatient);
            CheckAndReportVitalStatus(checker, elderlyVitals, elderlyPatient);
        }

        private static PatientProfile CreateElderlyPatient() {
            return new PatientProfile {
                Age = 70,
                Name = "John Smith",
                MedicalConditions = "Hypertension"
            };
        }

        private static void CheckAndReportVitalStatus(VitalsChecker checker, VitalReading vitals, PatientProfile profile = null) {
            if (checker.AreAllVitalsWithinRange(vitals, profile)) {
                var message = profile != null
                    ? "All vitals are within normal range for this patient profile"
                    : "All vitals are within normal range";
                Console.WriteLine(message);
            }
        }
    }
}