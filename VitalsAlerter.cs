using System;

namespace HealthMonitor
{
    /// <summary>
    /// Class that alerts when vitals are out of range
    /// </summary>
    public class VitalsAlerter
    {
        // Number of times to blink during an alert
        private const int DefaultBlinkCount = 6;

        /// <summary>
        /// Alerts when a vital reading is out of normal range
        /// </summary>
        /// <param name="reading">The vital reading to check</param>
        public virtual void Alert(VitalReading reading)
        {
            if (!reading.IsWithinRange)
            {
                Console.WriteLine($"{reading.VitalSign.Name} critical!");
                BlinkAlert();
            }
        }

        /// <summary>
        /// Creates a blinking alert on the console
        /// </summary>
        /// <param name="times">Number of times to blink the alert</param>
        private void BlinkAlert(int times = DefaultBlinkCount)
        {
            for (int i = 0; i < times; i++)
            {
                Console.Write("\r* ");
                System.Threading.Thread.Sleep(1000);
                Console.Write("\r *");
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
