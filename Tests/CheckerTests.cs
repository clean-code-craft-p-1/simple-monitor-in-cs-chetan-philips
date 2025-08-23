using System;
using System.Diagnostics;
using HealthMonitor.Core;
using HealthMonitor.Models;

namespace HealthMonitor.Tests {
    /// <summary>
    /// Comprehensive test suite for the Health Monitor System.
    /// Contains tests for vital sign checking functionality, boundary conditions, and edge cases.
    /// </summary>
    public static class CheckerTests {
        /// <summary>
        /// Runs all test cases and validates system functionality.
        /// </summary>
        public static void RunAllTests() {
            TestNormalVitals();
            TestBoundaryConditions();
            TestOutOfRangeVitals();
            TestPatientSpecificRanges();
            TestAlerterBehavior();
            TestNullInputHandling();
            Console.WriteLine("? All tests completed successfully!");
        }

        /// <summary>
        /// Tests that normal vital signs are correctly identified as within range.
        /// </summary>
        static void TestNormalVitals() {
            var checker = new VitalsChecker(new TestAlerter());
            var normalVitals = new VitalReading(98.6f, 72, 95);

            Debug.Assert(checker.AreAllVitalsWithinRange(normalVitals),
                "Normal vitals should be within range");
        }

        /// <summary>
        /// Tests boundary conditions at the edge of normal ranges.
        /// </summary>
        static void TestBoundaryConditions() {
            var checker = new VitalsChecker(new TestAlerter());

            // Test minimum boundary values
            var minBoundary = new VitalReading(95.0f, 60, 90);
            Debug.Assert(checker.AreAllVitalsWithinRange(minBoundary),
                "Minimum boundary values should be within range");

            // Test maximum boundary values
            var maxBoundary = new VitalReading(102.0f, 100, 100);
            Debug.Assert(checker.AreAllVitalsWithinRange(maxBoundary),
                "Maximum boundary values should be within range");

            // Test just below minimum
            var belowMin = new VitalReading(94.9f, 59, 89);
            Debug.Assert(!checker.AreAllVitalsWithinRange(belowMin),
                "Values below minimum should be out of range");

            // Test just above maximum
            var aboveMax = new VitalReading(102.1f, 101, 101);
            Debug.Assert(!checker.AreAllVitalsWithinRange(aboveMax),
                "Values above maximum should be out of range");
        }

        /// <summary>
        /// Tests that clearly out-of-range vitals are correctly identified.
        /// </summary>
        static void TestOutOfRangeVitals() {
            var checker = new VitalsChecker(new TestAlerter());

            // Test high temperature
            var highTemp = new VitalReading(105.0f, 72, 95);
            Debug.Assert(!checker.AreAllVitalsWithinRange(highTemp),
                "High temperature should be out of range");

            // Test low pulse
            var lowPulse = new VitalReading(98.6f, 50, 95);
            Debug.Assert(!checker.AreAllVitalsWithinRange(lowPulse),
                "Low pulse should be out of range");

            // Test low oxygen saturation
            var lowOxygen = new VitalReading(98.6f, 72, 85);
            Debug.Assert(!checker.AreAllVitalsWithinRange(lowOxygen),
                "Low oxygen saturation should be out of range");
        }

        /// <summary>
        /// Tests patient-specific range adjustments based on age and conditions.
        /// </summary>
        static void TestPatientSpecificRanges() {
            var checker = new VitalsChecker(new TestAlerter());

            // Test elderly patient with slightly lower temperature
            var elderlyPatient = new PatientProfile { Age = 70 };
            var elderlyVitals = new VitalReading(94.5f, 75, 92);
            Debug.Assert(checker.AreAllVitalsWithinRange(elderlyVitals, elderlyPatient),
                "Elderly patient should have adjusted temperature range");

            // Test child with higher pulse rate
            var childPatient = new PatientProfile { Age = 8 };
            var childVitals = new VitalReading(98.6f, 110, 95);
            Debug.Assert(checker.AreAllVitalsWithinRange(childVitals, childPatient),
                "Child should have adjusted pulse rate range");

            // Test COPD patient with lower oxygen saturation
            var copdPatient = new PatientProfile { MedicalConditions = "COPD" };
            var copdVitals = new VitalReading(98.6f, 72, 87);
            Debug.Assert(checker.AreAllVitalsWithinRange(copdVitals, copdPatient),
                "COPD patient should have adjusted oxygen saturation range");
        }

        /// <summary>
        /// Tests that the alerter is properly triggered for out-of-range values.
        /// </summary>
        static void TestAlerterBehavior() {
            var testAlerter = new TestAlerter();
            var checker = new VitalsChecker(testAlerter);

            var outOfRangeVitals = new VitalReading(104.0f, 110, 85);
            checker.CheckVitals(outOfRangeVitals);

            Debug.Assert(testAlerter.AlertCount == 3,
                $"Expected 3 alerts, got {testAlerter.AlertCount}");
        }

        /// <summary>
        /// Tests proper handling of null inputs and edge cases.
        /// </summary>
        static void TestNullInputHandling() {
            var checker = new VitalsChecker(new TestAlerter());

            // Test null vitals
            Debug.Assert(!checker.AreAllVitalsWithinRange(null),
                "Null vitals should return false");

            try {
                checker.CheckVitals(null);
                Debug.Assert(false, "CheckVitals should throw ArgumentNullException for null input");
            } catch (ArgumentNullException) {
                // Expected behavior
            }
        }
    }
}