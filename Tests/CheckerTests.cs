using System;
using HealthMonitor.Core;
using HealthMonitor.Models;

namespace HealthMonitor.Tests {
    /// <summary>
    /// Comprehensive tests for the VitalsChecker functionality.
    /// Tests normal ranges, boundary conditions, patient-specific adjustments, and error handling.
    /// </summary>
    public static class CheckerTests {
        public static void RunAllTests() {
            TestNormalVitals();
            TestBoundaryConditions();
            TestOutOfRangeVitals();
            TestPatientSpecificRanges();
            TestAlerterBehavior();
            TestNullInputHandling();
        }

        /// <summary>
        /// Tests that normal vital signs are correctly identified as within range.
        /// </summary>
        public static void TestNormalVitals() {
            var alerter = new VitalSignAlerterTests();
            var checker = new VitalsChecker(alerter);

            var normalVitals = new VitalReading(98.6f, 75, 95, 120f, 80f);
            
            if (!checker.AreAllVitalsWithinRange(normalVitals)) {
                throw new Exception("Normal vitals should be within range");
            }
        }

        /// <summary>
        /// Tests boundary conditions at the edge of normal ranges.
        /// </summary>
        public static void TestBoundaryConditions() {
            var alerter = new VitalSignAlerterTests();
            var checker = new VitalsChecker(alerter);

            // Test boundary values
            var boundaryVitals1 = new VitalReading(95.0f, 60, 90, 90f, 60f);
            var boundaryVitals2 = new VitalReading(102.0f, 100, 100, 140f, 90f);
            
            if (!checker.AreAllVitalsWithinRange(boundaryVitals1)) {
                throw new Exception("Lower boundary vitals should be within range");
            }
            
            if (!checker.AreAllVitalsWithinRange(boundaryVitals2)) {
                throw new Exception("Upper boundary vitals should be within range");
            }
            
            // Test just outside boundaries
            var outsideBoundary1 = new VitalReading(94.9f, 59, 89, 89f, 59f);
            var outsideBoundary2 = new VitalReading(102.1f, 101, 101, 141f, 91f);
            
            if (checker.AreAllVitalsWithinRange(outsideBoundary1)) {
                throw new Exception("Below boundary vitals should be out of range");
            }
            
            if (checker.AreAllVitalsWithinRange(outsideBoundary2)) {
                throw new Exception("Above boundary vitals should be out of range");
            }
        }

        /// <summary>
        /// Tests that clearly out-of-range vitals are correctly identified.
        /// </summary>
        public static void TestOutOfRangeVitals() {
            var alerter = new VitalSignAlerterTests();
            var checker = new VitalsChecker(alerter);

            // Test clearly out of range values
            var highVitals = new VitalReading(104.0f, 120, 85, 160f, 100f);
            var lowVitals = new VitalReading(90.0f, 50, 85, 70f, 40f);
            
            if (checker.AreAllVitalsWithinRange(highVitals)) {
                throw new Exception("High vitals should be out of range");
            }
            
            if (checker.AreAllVitalsWithinRange(lowVitals)) {
                throw new Exception("Low vitals should be out of range");
            }
            
            // Test mixed scenarios
            var mixedVitals = new VitalReading(98.6f, 120, 95, 120f, 80f);
            
            if (checker.AreAllVitalsWithinRange(mixedVitals)) {
                throw new Exception("Mixed vitals with one out of range should be overall out of range");
            }
        }

        /// <summary>
        /// Tests patient-specific range adjustments based on age and conditions.
        /// </summary>
        public static void TestPatientSpecificRanges() {
            var alerter = new VitalSignAlerterTests();
            var checker = new VitalsChecker(alerter);

            // Test elderly patient with adjusted ranges
            var elderlyPatient = new PatientProfile { Age = 70, Name = "John Doe" };
            var elderlyVitals = new VitalReading(94.0f, 58, 92, 145f, 92f);

            if (!checker.AreAllVitalsWithinRange(elderlyVitals, elderlyPatient)) {
                throw new Exception("Elderly patient vitals should be within adjusted range");
            }

            // Test child patient
            var childPatient = new PatientProfile { Age = 8, Name = "Jane Doe" };
            var childVitals = new VitalReading(103.0f, 110, 96, 115f, 75f);

            if (!checker.AreAllVitalsWithinRange(childVitals, childPatient)) {
                throw new Exception("Child patient vitals should be within adjusted range");
            }

            // Test COPD patient
            var copdPatient = new PatientProfile { Age = 65, Name = "Bob Smith", MedicalConditions = "COPD" };
            var copdVitals = new VitalReading(98.6f, 75, 87, 130f, 85f);

            if (!checker.AreAllVitalsWithinRange(copdVitals, copdPatient)) {
                throw new Exception("COPD patient vitals should be within adjusted range");
            }
        }

        /// <summary>
        /// Tests that the alerter is properly triggered for out-of-range values.
        /// </summary>
        public static void TestAlerterBehavior() {
            var alerter = new VitalSignAlerterTests();
            var checker = new VitalsChecker(alerter);

            alerter.Reset();
            var abnormalVitals = new VitalReading(104.0f, 120, 85, 160f, 100f);
            checker.CheckVitals(abnormalVitals);

            if (alerter.AlertCount == 0) {
                throw new Exception("Alerter should have been called for abnormal vitals");
            }
        }

        /// <summary>
        /// Tests proper handling of null inputs and edge cases.
        /// </summary>
        public static void TestNullInputHandling() {
            var alerter = new VitalSignAlerterTests();
            var checker = new VitalsChecker(alerter);

            // Test with null profile
            var normalVitals = new VitalReading(98.6f, 75, 95, 120f, 80f);
            
            if (!checker.AreAllVitalsWithinRange(normalVitals, null)) {
                throw new Exception("Normal vitals should be within range even with null profile");
            }

            // Test with profile having null age
            var profileWithNullAge = new PatientProfile { Age = null, Name = "Test Patient" };
            
            if (!checker.AreAllVitalsWithinRange(normalVitals, profileWithNullAge)) {
                throw new Exception("Normal vitals should be within range with null age");
            }
        }
    }
}