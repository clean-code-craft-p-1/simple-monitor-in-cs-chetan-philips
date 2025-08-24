using System;
using HealthMonitor.Core;

using HealthMonitor.Models;
using HealthMonitor.VitalSigns;

namespace HealthMonitor.Tests {
    /// <summary>
    /// Comprehensive test suite with simplified alerting.
    /// </summary>
    public static class CheckerTests {
        public static void RunAllTests() {
            TestNormalVitals();
            TestAbnormalVitals();
            TestBoundaryConditions();
            TestPatientSpecificRanges();
            TestExtensibility();
            Console.WriteLine("All tests completed successfully!");
        }

        public static void TestNormalVitals() {
            var checker = CreateTestChecker();
            var normalVitals = new VitalReading(98.6f, 75f, 95f, 120f, 80f);

            if (!checker.AreAllVitalsWithinRange(normalVitals)) {
                throw new Exception("Normal vitals should be within range");
            }
        }

        public static void TestAbnormalVitals() {
            int alertCount = 0;
            var checker = new VitalsChecker((name, value, unit) => alertCount++);
            var abnormalVitals = new VitalReading(104f, 110f, 85f, 160f, 100f);

            checker.CheckVitals(abnormalVitals);
            if (alertCount == 0) {
                throw new Exception("Abnormal vitals should trigger alerts");
            }
        }

        public static void TestBoundaryConditions() {
            var checker = CreateTestChecker();

            // Test boundaries
            var lowerBoundary = new VitalReading(95f, 60f, 90f, 90f, 60f);
            if (!checker.AreAllVitalsWithinRange(lowerBoundary)) {
                throw new Exception("Lower boundary vitals should be within range");
            }

            var upperBoundary = new VitalReading(102f, 100f, 100f, 140f, 90f);
            if (!checker.AreAllVitalsWithinRange(upperBoundary)) {
                throw new Exception("Upper boundary vitals should be within range");
            }

            var belowBoundary = new VitalReading(94f, 59f, 89f, 89f, 59f);
            if (checker.AreAllVitalsWithinRange(belowBoundary)) {
                throw new Exception("Below boundary vitals should be out of range");
            }
        }

        public static void TestPatientSpecificRanges() {
            var checker = CreateTestChecker();

            // Elderly patient
            var elderly = new PatientProfile { Age = 70 };
            var elderlyVitals = new VitalReading(94f, 58f, 92f, 145f, 92f);
            if (!checker.AreAllVitalsWithinRange(elderlyVitals, elderly)) {
                throw new Exception("Elderly patient vitals should be within adjusted range");
            }

            // Child patient
            var child = new PatientProfile { Age = 8 };
            var childVitals = new VitalReading(103f, 110f, 96f, 115f, 75f);
            if (!checker.AreAllVitalsWithinRange(childVitals, child)) {
                throw new Exception("Child patient vitals should be within adjusted range");
            }

            // COPD patient
            var copd = new PatientProfile { Age = 65, MedicalConditions = "COPD" };
            var copdVitals = new VitalReading(98.6f, 75f, 87f, 130f, 85f);
            if (!checker.AreAllVitalsWithinRange(copdVitals, copd)) {
                throw new Exception("COPD patient vitals should be within adjusted range");
            }
        }

        public static void TestExtensibility() {
            int alertCount = 0;
            var checker = new VitalsChecker((name, value, unit) => alertCount++);
            checker.RegisterVitalSign(new RespiratoryRate());

            var vitals = new VitalReading();
            vitals.SetReading("Respiratory Rate", 25f); // High
            checker.CheckVitals(vitals);

            if (alertCount == 0) {
                throw new Exception("High respiratory rate should trigger alert");
            }
        }

        private static VitalsChecker CreateTestChecker() {
            return new VitalsChecker((name, value, unit) => { /* Silent for testing */ });
        }
    }

    /// <summary>
    /// Example respiratory rate vital sign for testing extensibility.
    /// </summary>
    public class RespiratoryRate : AgeBasedVitalSign {
        public override string Name => "Respiratory Rate";
        public override string Unit => "breaths/min";

        protected override (float min, float max) GetAgeSpecificRange(int? age) {
            return age switch {
                < 12 => (20f, 30f),     // Child
                >= 65 => (12f, 28f),    // Elderly
                _ => (12f, 20f)         // Adult
            };
        }
    }
}