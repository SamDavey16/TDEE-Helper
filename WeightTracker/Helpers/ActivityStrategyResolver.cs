using Microsoft.Extensions.DependencyInjection;
using WeightTracker.Interfaces;
using WeightTracker.Services.ActivityLevelStrategies;

namespace WeightTracker.Helpers
{
    public class ActivityStrategyResolver
    {
        private readonly IServiceProvider _provider;

        public ActivityStrategyResolver(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IActivityLevelStrategy Resolve(string input) =>
            input switch
            {
                "Sedentry" => _provider.GetRequiredService<Sedentry>(),
                "Moderate" => _provider.GetRequiredService<Moderate>(),
                "VeryActive" => _provider.GetRequiredService<VeryActive>(),
                _ => throw new ArgumentException("Invalid activity level")
            };
    }
}
