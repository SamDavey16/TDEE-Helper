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
                "1" => _provider.GetRequiredService<Sedentry>(),
                "2" => _provider.GetRequiredService<Moderate>(),
                "3" => _provider.GetRequiredService<VeryActive>(),
                _ => throw new ArgumentException("Invalid activity level")
            };
    }
}
