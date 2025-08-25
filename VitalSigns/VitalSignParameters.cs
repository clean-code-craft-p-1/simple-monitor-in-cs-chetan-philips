namespace HealthMonitor.VitalSigns {
    /// <summary>
    /// Structure to hold vital sign range parameters to eliminate parameter duplication
    /// </summary>
    internal readonly struct VitalSignParameters
    {
        public readonly string Name;
        public readonly string Unit;
        public readonly float ChildMin;
        public readonly float ChildMax;
        public readonly float AdultMin;
        public readonly float AdultMax;
        public readonly float ElderlyMin;
        public readonly float ElderlyMax;

        public VitalSignParameters(
            string name, 
            string unit,
            float childMin, float childMax,
            float adultMin, float adultMax,
            float elderlyMin, float elderlyMax)
        {
            Name = name;
            Unit = unit;
            ChildMin = childMin;
            ChildMax = childMax;
            AdultMin = adultMin;
            AdultMax = adultMax;
            ElderlyMin = elderlyMin;
            ElderlyMax = elderlyMax;
        }
    }
}
