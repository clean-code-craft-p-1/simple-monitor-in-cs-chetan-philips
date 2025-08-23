using System;

using HealthMonitor.Core;
using HealthMonitor.Infrastructure;
using HealthMonitor.Models;

namespace HealthMonitor {
    /// <summary>
    /// Main class for checking vitals, maintains backward compatibility with original interface
    /// </summary>
    public static class Checker {
        /// <summary>
        /// Checks vital signs and alerts if any are out of range.
        /// </summary>
        public static void Main() {
            Console.WriteLine("Health Monitor System Starting...");

            // Run tests first
            Console.WriteLine("Running tests...");
            HealthMonitor.Tests.CheckerTests.RunAllTests();
            Console.WriteLine();

            var alerter = new VitalsAlerter();
            var checker = new VitalsChecker(alerter);

            // Test with normal vitals
            var normalVitals = new VitalReading(98.6f, 72, 95);
            Console.WriteLine("Checking normal vitals...");
            checker.CheckVitals(normalVitals);

            // Test with abnormal vitals
            var abnormalVitals = new VitalReading(104.0f, 110, 85);
            Console.WriteLine("Checking abnormal vitals...");
            checker.CheckVitals(abnormalVitals);

            // Test with patient profile
            var patientProfile = new PatientProfile { Age = 25, Name = "John Doe" };
            Console.WriteLine("Checking vitals with patient profile...");
            checker.CheckVitals(normalVitals, patientProfile);

            Console.WriteLine("Health Monitor System Complete.");
        }
    }
}