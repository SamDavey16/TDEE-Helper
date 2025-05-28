using WeightTracker.Interfaces;

namespace WeightTracker.Services.ActivityLevelStrategies
{
    public class Moderate : IActivityLevelStrategy
    {
        public double GetActivityMultiplier() => 1.55;
        public string Name => "Moderately Active - 3–5 days/week";
    }
}
