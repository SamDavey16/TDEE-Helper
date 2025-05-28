using WeightTracker.Interfaces;

namespace WeightTracker.Services.ActivityLevelStrategies
{
    public class VeryActive : IActivityLevelStrategy
    {
        public double GetActivityMultiplier() => 1.9;
        public string Name => "Very Active - 6–7 days/week";
    }
}
