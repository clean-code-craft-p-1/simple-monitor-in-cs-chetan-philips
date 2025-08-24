using System;
using HealthMonitor.Core;
using HealthMonitor.Models;
using HealthMonitor.VitalSigns;

namespace HealthMonitor.Tests {
    /// <summary>
    /// Comprehensive test suite with helper methods to eliminate duplication.
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
            var checker = CreateTestVitalsChecker();
            var normalVitals = new VitalReading(98.6f, 75f, 95f, 120f, 80f);

            AssertVitalsInRange(checker, normalVitals, "Normal vitals should be within range");
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
            var checker = CreateTestVitalsChecker();

            // Lower boundaries
            var lowerVitals = new VitalReading(95f, 60f, 90f, 90f, 60f);
            AssertVitalsInRange(checker, lowerVitals, "Lower boundary vitals should be within range");

            // Upper boundaries  
            var upperVitals = new VitalReading(102f, 100f, 100f, 140f, 90f);
            AssertVitalsInRange(checker, upperVitals, "Upper boundary vitals should be within range");

            // Below boundaries
            var belowVitals = new VitalReading(94f, 59f, 89f, 89f, 59f);
            AssertVitalsOutOfRange(checker, belowVitals, "Below boundary vitals should be out of range");
        }

        public static void TestPatientSpecificRanges() {
            var checker = CreateTestVitalsChecker();

            // Elderly patient
            var elderly = new PatientProfile { Age = 70 };
            var elderlyVitals = new VitalReading(94f, 58f, 92f, 145f, 92f);
            AssertVitalsInRange(checker, elderlyVitals, elderly, "Elderly patient vitals should be within adjusted range");

            // Child patient
            var child = new PatientProfile { Age = 8 };
            var childVitals = new VitalReading(103f, 110f, 96f, 115f, 75f);
            AssertVitalsInRange(checker, childVitals, child, "Child patient vitals should be within adjusted range");

            // COPD patient
            var copd = new PatientProfile { Age = 65, MedicalConditions = "COPD" };
            var copdVitals = new VitalReading(98.6f, 75f, 87f, 130f, 85f);
            AssertVitalsInRange(checker, copdVitals, copd, "COPD patient vitals should be within adjusted range");
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

        // Helper methods to eliminate duplication
        private static VitalsChecker CreateTestVitalsChecker() {
            return new VitalsChecker((name, value, unit) => { /* Silent for testing */ });
        }

        private static void AssertVitalsInRange(VitalsChecker checker, VitalReading vitals, string message) {
            AssertVitalsInRange(checker, vitals, null, message);
        }

        private static void AssertVitalsInRange(VitalsChecker checker, VitalReading vitals, PatientProfile profile, string message) {
            if (!checker.AreAllVitalsWithinRange(vitals, profile)) {
                throw new Exception(message);
            }
        }

        private static void AssertVitalsOutOfRange(VitalsChecker checker, VitalReading vitals, string message) {
            if (checker.AreAllVitalsWithinRange(vitals)) {
                throw new Exception(message);
            }
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