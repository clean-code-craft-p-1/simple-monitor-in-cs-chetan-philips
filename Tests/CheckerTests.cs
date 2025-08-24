using System;
using HealthMonitor.Core;
using HealthMonitor.Models;

namespace HealthMonitor.Tests {
    /// <summary>
    /// Comprehensive tests for the simplified VitalsChecker functionality.
    /// </summary>
    public static class CheckerTests {
        public static void RunAllTests() {
            TestNormalVitals();
            TestAbnormalVitals();
            TestBoundaryConditions();
            TestPatientSpecificRanges();
            TestExtensibility();
            TestBloodPressureHandling();
            Console.WriteLine("All tests completed successfully!");
        }

        public static void TestNormalVitals() {
            var alerter = new TestAlerter();
            var checker = new VitalsChecker(alerter);
            var normalVitals = new VitalReading(98.6f, 72f, 95f, 120f, 80f);

            if (!checker.AreAllVitalsWithinRange(normalVitals)) {
                throw new Exception("Normal vitals should be within range");
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
            var alerter = new TestAlerter();
            var checker = new VitalsChecker(alerter);

            // Test lower boundaries
            var lowerBoundary = new VitalReading(95f, 60f, 90f, 90f, 60f);
            if (!checker.AreAllVitalsWithinRange(lowerBoundary)) {
                throw new Exception("Lower boundary vitals should be within range");
            }

            // Test upper boundaries
            var upperBoundary = new VitalReading(102f, 100f, 100f, 140f, 90f);
            if (!checker.AreAllVitalsWithinRange(upperBoundary)) {
                throw new Exception("Upper boundary vitals should be within range");
            }

            // Test below boundaries
            var belowBoundary = new VitalReading(94f, 59f, 89f, 89f, 59f);
            if (checker.AreAllVitalsWithinRange(belowBoundary)) {
                throw new Exception("Below boundary vitals should be out of range");
            }
        }

        public static void TestPatientSpecificRanges() {
            var alerter = new TestAlerter();
            var checker = new VitalsChecker(alerter);

            // Test elderly patient
            var elderlyPatient = new PatientProfile { Age = 70 };
            var elderlyVitals = new VitalReading(94f, 58f, 92f, 145f, 92f);
            if (!checker.AreAllVitalsWithinRange(elderlyVitals, elderlyPatient)) {
                throw new Exception("Elderly patient vitals should be within adjusted range");
            }

            // Test child patient
            var childPatient = new PatientProfile { Age = 8 };
            var childVitals = new VitalReading(103f, 110f, 96f, 115f, 75f);
            if (!checker.AreAllVitalsWithinRange(childVitals, childPatient)) {
                throw new Exception("Child patient vitals should be within adjusted range");
            }

            // Test COPD patient
            var copdPatient = new PatientProfile { MedicalConditions = "COPD" };
            var copdVitals = new VitalReading(98.6f, 75f, 87f, 130f, 85f);
            if (!checker.AreAllVitalsWithinRange(copdVitals, copdPatient)) {
                throw new Exception("COPD patient vitals should be within adjusted range");
            }
        }

        public static void TestBloodPressureHandling() {
            var alerter = new TestAlerter();
            var checker = new VitalsChecker(alerter);

            // Test individual blood pressure components
            var vitals = new VitalReading();
            vitals.SetReading("Systolic Blood Pressure", 160f); // High
            vitals.SetReading("Diastolic Blood Pressure", 80f);  // Normal

            if (checker.AreAllVitalsWithinRange(vitals)) {
                throw new Exception("High systolic should be out of range");
            }

            // Test normal blood pressure components
            vitals = new VitalReading();
            vitals.SetReading("Systolic Blood Pressure", 120f);
            vitals.SetReading("Diastolic Blood Pressure", 80f);

            if (!checker.AreAllVitalsWithinRange(vitals)) {
                throw new Exception("Normal blood pressure should be within range");
            }
        }

        public static void TestExtensibility() {
            var alerter = new TestAlerter();
            var checker = new VitalsChecker(alerter);

            // Register new vital sign
            checker.RegisterVitalSign(new RespiratoryRate());

            var vitals = new VitalReading();
            vitals.SetReading("Respiratory Rate", 16f); // Normal

            if (!checker.AreAllVitalsWithinRange(vitals)) {
                throw new Exception("Normal respiratory rate should be within range");
            }

            vitals.SetReading("Respiratory Rate", 35f); // High
            if (checker.AreAllVitalsWithinRange(vitals)) {
                throw new Exception("High respiratory rate should be out of range");
            }
        }
    }

    /// <summary>
    /// Test alerter implementation.
    /// </summary>
    public class TestAlerter : IVitalSignAlerter {
        public int AlertCount { get; private set; }

        public void Alert(string vitalName, string value, string unit) {
            AlertCount++;
        }
    }

    /// <summary>
    /// Example of easily adding new vital signs.
    /// </summary>
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