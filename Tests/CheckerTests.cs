using System;
using System.Diagnostics;

using HealthMonitor.Core;
using HealthMonitor.Models;

namespace HealthMonitor.Tests {
    /// <summary>
    /// Test suite for the Health Monitor System
    /// Contains comprehensive tests for vital sign checking functionality
    /// </summary>
    public static class CheckerTests
    {
        /// <summary>
        /// Runs all test methods and reports results
        /// </summary>
        public static void RunAllTests()
        {
            TestVitalsWithinRange();
            TestVitalsOutOfRange();
            TestBoundaryConditions();
            TestPatientSpecificRanges();
            TestAlerterBehavior();
            TestAllCombinations();
            Console.WriteLine("All tests passed!");
        }

        static void TestVitalsWithinRange()
        {
            var checker = new VitalsChecker(new TestAlerter());
            var vitals = new VitalReading(98.1f, 72, 95);
            Debug.Assert(checker.AreAllVitalsWithinRange(vitals));
        }

        static void TestVitalsOutOfRange()
        {
            var checker = new VitalsChecker(new TestAlerter());
            
            var highTemp = new VitalReading(102.5f, 72, 95);
            Debug.Assert(!checker.AreAllVitalsWithinRange(highTemp));
            
            var highPulse = new VitalReading(98.1f, 110, 95);
            Debug.Assert(!checker.AreAllVitalsWithinRange(highPulse));
            
            var lowOxygen = new VitalReading(98.1f, 72, 85);
            Debug.Assert(!checker.AreAllVitalsWithinRange(lowOxygen));
        }

        static void TestBoundaryConditions()
        {
            var checker = new VitalsChecker(new TestAlerter());
            
            var minBoundary = new VitalReading(95.0f, 60, 90);
            Debug.Assert(checker.AreAllVitalsWithinRange(minBoundary));
            
            var maxBoundary = new VitalReading(102.0f, 100, 100);
            Debug.Assert(checker.AreAllVitalsWithinRange(maxBoundary));
            
            var belowMin = new VitalReading(94.9f, 59, 89);
            Debug.Assert(!checker.AreAllVitalsWithinRange(belowMin));
            
            var aboveMax = new VitalReading(102.1f, 101, 101);
            Debug.Assert(!checker.AreAllVitalsWithinRange(aboveMax));
        }

        static void TestPatientSpecificRanges()
        {
            var checker = new VitalsChecker(new TestAlerter());
            var youngPatient = new PatientProfile { Age = 15 };
            var elderlyPatient = new PatientProfile { Age = 70 };
            
            var vitals = new VitalReading(98.6f, 75, 98);
            Debug.Assert(checker.AreAllVitalsWithinRange(vitals, youngPatient));
            Debug.Assert(checker.AreAllVitalsWithinRange(vitals, elderlyPatient));
        }

        static void TestAlerterBehavior()
        {
            var testAlerter = new TestAlerter();
            var checker = new VitalsChecker(testAlerter);
            
            var outOfRangeVitals = new VitalReading(104.0f, 110, 85);
            checker.CheckVitals(outOfRangeVitals);
            
            Debug.Assert(testAlerter.AlertCount == 3);
        }

        static void TestAllCombinations()
        {
            var checker = new VitalsChecker(new TestAlerter());
            
            var combinations = new[]
            {
                new VitalReading(94.0f, 70, 95),   // Only temp out
                new VitalReading(98.0f, 110, 95),  // Only pulse out
                new VitalReading(98.0f, 70, 85),   // Only oxygen out
                new VitalReading(94.0f, 110, 95),  // Temp and pulse out
                new VitalReading(94.0f, 70, 85),   // Temp and oxygen out
                new VitalReading(98.0f, 110, 85),  // Pulse and oxygen out
                new VitalReading(94.0f, 110, 85)   // All out
            };

            foreach (var combo in combinations)
            {
                Debug.Assert(!checker.AreAllVitalsWithinRange(combo));
            }
        }
    }
}