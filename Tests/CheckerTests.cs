using System;
using HealthMonitor.Core;
using HealthMonitor.Models;

namespace HealthMonitor.Tests {
    /// <summary>
    /// Comprehensive tests for the VitalsChecker functionality.
    /// </summary>
    public static class CheckerTests {
        public static void RunAllTests() {
            TestNormalVitals();
            TestAbnormalVitals();
            TestPatientSpecificRanges();
            TestExtensibility();
            TestBoundaryConditions();
            TestOutOfRangeVitals();
        }

        public static void TestNormalVitals() {
            var alerter = new TestAlerter();
            var checker = new VitalsChecker(alerter);

            var normalVitals = new VitalReading(98.6f, 72f, 95f, 120f, 80f);
            checker.CheckVitals(normalVitals);

            if (alerter.AlertCount > 0) {
                throw new Exception("Normal vitals should not trigger alerts");
            }
        }

        public static void TestAbnormalVitals() {
            var alerter = new TestAlerter();
            var checker = new VitalsChecker(alerter);

            var abnormalVitals = new VitalReading(104f, 110f, 85f, 160f, 100f);
            checker.CheckVitals(abnormalVitals);

            if (alerter.AlertCount == 0) {
                throw new Exception("Abnormal vitals should trigger alerts");
            }
        }

        public static void TestBoundaryConditions() {
            TestLowerBoundaries();
            TestUpperBoundaries();
            TestBelowBoundaries();
            TestAboveBoundaries();
        }

        private static void TestLowerBoundaries() {
            var alerter = new TestAlerter();
            var checker = new VitalsChecker(alerter);
            var boundaryVitals = new VitalReading(95.0f, 60f, 90f, 90f, 60f);
            
            if (!checker.AreAllVitalsWithinRange(boundaryVitals)) {
                throw new Exception("Lower boundary vitals should be within range");
            }
        }

        private static void TestUpperBoundaries() {
            var alerter = new TestAlerter();
            var checker = new VitalsChecker(alerter);
            var boundaryVitals = new VitalReading(102.0f, 100f, 100f, 140f, 90f);
            
            if (!checker.AreAllVitalsWithinRange(boundaryVitals)) {
                throw new Exception("Upper boundary vitals should be within range");
            }
        }

        private static void TestBelowBoundaries() {
            var alerter = new TestAlerter();
            var checker = new VitalsChecker(alerter);
            var outsideBoundary = new VitalReading(94.9f, 59f, 89f, 89f, 59f);
            
            if (checker.AreAllVitalsWithinRange(outsideBoundary)) {
                throw new Exception("Below boundary vitals should be out of range");
            }
        }

        private static void TestAboveBoundaries() {
            var alerter = new TestAlerter();
            var checker = new VitalsChecker(alerter);
            var outsideBoundary = new VitalReading(102.1f, 101f, 101f, 141f, 91f);
            
            if (checker.AreAllVitalsWithinRange(outsideBoundary)) {
                throw new Exception("Above boundary vitals should be out of range");
            }
        }

        public static void TestOutOfRangeVitals() {
            TestHighVitals();
            TestLowVitals();
            TestMixedVitals();
        }

        private static void TestHighVitals() {
            var alerter = new TestAlerter();
            var checker = new VitalsChecker(alerter);
            var highVitals = new VitalReading(104.0f, 120f, 85f, 160f, 100f);
            
            if (checker.AreAllVitalsWithinRange(highVitals)) {
                throw new Exception("High vitals should be out of range");
            }
        }

        private static void TestLowVitals() {
            var alerter = new TestAlerter();
            var checker = new VitalsChecker(alerter);
            var lowVitals = new VitalReading(90.0f, 50f, 85f, 70f, 40f);
            
            if (checker.AreAllVitalsWithinRange(lowVitals)) {
                throw new Exception("Low vitals should be out of range");
            }
        }

        private static void TestMixedVitals() {
            var alerter = new TestAlerter();
            var checker = new VitalsChecker(alerter);
            var mixedVitals = new VitalReading(98.6f, 120f, 95f, 120f, 80f);
            
            if (checker.AreAllVitalsWithinRange(mixedVitals)) {
                throw new Exception("Mixed vitals with one out of range should be overall out of range");
            }
        }

        public static void TestPatientSpecificRanges() {
            TestElderlyPatient();
            TestChildPatient();
            TestCOPDPatient();
        }

        private static void TestElderlyPatient() {
            var alerter = new TestAlerter();
            var checker = new VitalsChecker(alerter);
            var elderlyPatient = new PatientProfile { Age = 70, Name = "John Doe" };
            var elderlyVitals = new VitalReading(94.0f, 58f, 92f, 145f, 92f);

            if (!checker.AreAllVitalsWithinRange(elderlyVitals, elderlyPatient)) {
                throw new Exception("Elderly patient vitals should be within adjusted range");
            }
        }

        private static void TestChildPatient() {
            var alerter = new TestAlerter();
            var checker = new VitalsChecker(alerter);
            var childPatient = new PatientProfile { Age = 8, Name = "Jane Doe" };
            var childVitals = new VitalReading(103f, 110f, 96f, 115f, 75f);
            
            checker.CheckVitals(childVitals, childPatient);

            if (alerter.AlertCount > 0) {
                throw new Exception("Child vitals should be within adjusted range");
            }
        }

        private static void TestCOPDPatient() {
            var alerter = new TestAlerter();
            var checker = new VitalsChecker(alerter);
            var copdPatient = new PatientProfile { Age = 65, Name = "Bob Smith", MedicalConditions = "COPD" };
            var copdVitals = new VitalReading(98.6f, 75f, 87f, 130f, 85f);

            if (!checker.AreAllVitalsWithinRange(copdVitals, copdPatient)) {
                throw new Exception("COPD patient vitals should be within adjusted range");
            }
        }

        public static void TestExtensibility() {
            var alerter = new TestAlerter();
            var checker = new VitalsChecker(alerter);

            checker.RegisterVitalSign(new RespiratoryRate());

            var vitals = new VitalReading();
            vitals.SetReading("Respiratory Rate", 25f); // High
            checker.CheckVitals(vitals);

            if (alerter.AlertCount == 0) {
                throw new Exception("High respiratory rate should trigger alert");
            }
        }
    }

    public class TestAlerter : IVitalSignAlerter {
        public int AlertCount { get; private set; }

        public void Alert(string vitalName, string value, string unit) {
            AlertCount++;
        }
    }

    public class RespiratoryRate : IVitalSign {
        public string Name => "Respiratory Rate";
        public string Unit => "breaths/min";

        public bool IsWithinRange(float value, PatientProfile profile = null) {
            return value >= 12f && value <= 20f; // Normal adult range
        }
    }
}