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
            var normalVitals = new VitalReading(
                VitalRangeConstants.TEMP_NORMAL, 
                VitalRangeConstants.PULSE_NORMAL, 
                VitalRangeConstants.OXY_NORMAL, 
                VitalRangeConstants.SYS_NORMAL, 
                VitalRangeConstants.DIA_NORMAL
            );

            AssertVitalsInRange(checker, normalVitals, "Normal vitals should be within range");
        }

        public static void TestAbnormalVitals() {
            // Use our new approach that returns both checker and counter
            var (checker, getAlertCount) = CreateAlertCountingChecker();
            var abnormalVitals = new VitalReading(
                VitalRangeConstants.TEMP_HIGH, 
                VitalRangeConstants.PULSE_HIGH, 
                VitalRangeConstants.OXY_LOW, 
                VitalRangeConstants.SYS_HIGH, 
                VitalRangeConstants.DIA_HIGH
            );

            checker.CheckVitals(abnormalVitals);
            if (getAlertCount() == 0) {
                throw new InvalidOperationException("Abnormal vitals should trigger alerts");
            }
        }

        public static void TestBoundaryConditions() {
            var checker = CreateTestVitalsChecker();

            // Lower boundaries
            var lowerVitals = new VitalReading(
                VitalRangeConstants.TEMP_MIN_ADULT,
                VitalRangeConstants.PULSE_MIN_ADULT,
                VitalRangeConstants.OXY_MIN,
                VitalRangeConstants.SYS_MIN_ADULT,
                VitalRangeConstants.DIA_MIN_ADULT
            );
            AssertVitalsInRange(checker, lowerVitals, "Lower boundary vitals should be within range");

            // Upper boundaries  
            var upperVitals = new VitalReading(
                VitalRangeConstants.TEMP_MAX_ADULT,
                VitalRangeConstants.PULSE_MAX_ADULT,
                VitalRangeConstants.OXY_MAX,
                VitalRangeConstants.SYS_MAX_ADULT,
                VitalRangeConstants.DIA_MAX_ADULT
            );
            AssertVitalsInRange(checker, upperVitals, "Upper boundary vitals should be within range");

            // Below boundaries
            var belowVitals = new VitalReading(
                VitalRangeConstants.TEMP_MIN_ADULT - 1,
                VitalRangeConstants.PULSE_MIN_ADULT - 1,
                VitalRangeConstants.OXY_MIN - 1,
                VitalRangeConstants.SYS_MIN_ADULT - 1,
                VitalRangeConstants.DIA_MIN_ADULT - 1
            );
            AssertVitalsOutOfRange(checker, belowVitals, "Below boundary vitals should be out of range");
        }

        public static void TestPatientSpecificRanges() {
            var checker = CreateTestVitalsChecker();

            // Elderly patient
            var elderly = new PatientProfile { Age = VitalRangeConstants.ELDERLY_MIN_AGE + 5 };
            var elderlyVitals = new VitalReading(
                VitalRangeConstants.TEMP_MIN_ELDERLY,
                VitalRangeConstants.PULSE_MIN_ELDERLY + 3,
                VitalRangeConstants.OXY_MIN + 2,
                VitalRangeConstants.SYS_MAX_ELDERLY - 5,
                VitalRangeConstants.DIA_MAX_ELDERLY - 3
            );
            AssertVitalsInRange(checker, elderlyVitals, elderly, "Elderly patient vitals should be within adjusted range");

            // Child patient
            var child = new PatientProfile { Age = VitalRangeConstants.CHILD_MAX_AGE - 4 };
            var childVitals = new VitalReading(
                VitalRangeConstants.TEMP_MAX_CHILD,
                VitalRangeConstants.PULSE_MAX_CHILD - 10,
                VitalRangeConstants.OXY_MIN + 6,
                VitalRangeConstants.SYS_MAX_CHILD - 5,
                VitalRangeConstants.DIA_MAX_CHILD - 5
            );
            AssertVitalsInRange(checker, childVitals, child, "Child patient vitals should be within adjusted range");

            // COPD patient
            var copd = new PatientProfile { Age = VitalRangeConstants.ELDERLY_MIN_AGE, MedicalConditions = "COPD" };
            var copdVitals = new VitalReading(
                VitalRangeConstants.TEMP_NORMAL,
                VitalRangeConstants.PULSE_NORMAL,
                VitalRangeConstants.OXY_COPD,
                VitalRangeConstants.SYS_NORMAL,
                VitalRangeConstants.DIA_NORMAL
            );
            AssertVitalsInRange(checker, copdVitals, copd, "COPD patient vitals should be within adjusted range");
        }

        public static void TestExtensibility() {
            // Use our new approach that returns both checker and counter
            var (checker, getAlertCount) = CreateAlertCountingChecker();
            checker.RegisterVitalSign(new VitalSigns.RespiratoryRate());

            var vitals = new VitalReading();
            vitals.SetReading("Respiratory Rate", VitalRangeConstants.RESP_HIGH); // High
            checker.CheckVitals(vitals);

            if (getAlertCount() == 0) {
                throw new InvalidOperationException("High respiratory rate should trigger alert");
            }
        }

        // Helper methods to eliminate duplication
        private static VitalsChecker CreateTestVitalsChecker() {
            return new VitalsChecker((name, value, unit) => { /* Silent for testing */ });
        }

        // New approach - return both the checker and a function to get the current count
        private static (VitalsChecker checker, Func<int> getAlertCount) CreateAlertCountingChecker() {
            // Use a local variable that the lambda can safely capture and modify
            int localAlertCount = 0;
            
            // Create a checker with an action that modifies our local variable
            var checker = new VitalsChecker((name, value, unit) => localAlertCount++);
            
            // Return both the checker and a function to get the current count
            return (checker, () => localAlertCount);
        }

        private static void AssertVitalsInRange(VitalsChecker checker, VitalReading vitals, string message) {
            AssertVitalsInRange(checker, vitals, null, message);
        }

        private static void AssertVitalsInRange(VitalsChecker checker, VitalReading vitals, PatientProfile profile, string message) {
            if (!checker.AreAllVitalsWithinRange(vitals, profile)) {
                throw new InvalidOperationException(message);
            }
        }

        private static void AssertVitalsOutOfRange(VitalsChecker checker, VitalReading vitals, string message) {
            if (checker.AreAllVitalsWithinRange(vitals)) {
                throw new InvalidOperationException(message);
            }
        }
    }
}