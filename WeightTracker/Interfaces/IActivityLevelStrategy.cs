namespace WeightTracker.Interfaces
{
    public interface IActivityLevelStrategy
    {
        double GetActivityMultiplier();
        string Name { get; }
    }
}
