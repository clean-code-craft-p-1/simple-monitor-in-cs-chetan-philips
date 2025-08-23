namespace HealthMonitor.Core {
    /// <summary>
    /// Utility class for age-based patient classification.
    /// Provides consistent age thresholds across all vital sign implementations.
    /// </summary>
    public static class AgeClassifier {
        private const int CHILD_AGE_THRESHOLD = 12;
        private const int ELDERLY_AGE_THRESHOLD = 65;

        /// <summary>
        /// Determines if the given age represents a child.
        /// </summary>
        /// <param name="age">Age in years</param>
        /// <returns>True if age is less than 12 years</returns>
        public static bool IsChild(int age) => age < CHILD_AGE_THRESHOLD;

        /// <summary>
        /// Determines if the given age represents an elderly person.
        /// </summary>
        /// <param name="age">Age in years</param>
        /// <returns>True if age is 65 years or older</returns>
        public static bool IsElderly(int age) => age >= ELDERLY_AGE_THRESHOLD;
    }
}
