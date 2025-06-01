using WeightTracker.Interfaces;

namespace WeightTracker.Services.ActivityLevelStrategies
{
    public class Sedentry : IActivityLevelStrategy
    {
        public double GetActivityMultiplier() => 1.2;
        public string Name => "Sedentary (little or no exercise)";
    }
}
