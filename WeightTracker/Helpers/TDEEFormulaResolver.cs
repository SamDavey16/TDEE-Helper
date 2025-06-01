using Microsoft.Extensions.DependencyInjection;
using WeightTracker.Interfaces;
using WeightTracker.Services;

namespace WeightTracker.Helpers
{
    public class TDEEFormulaResolver
    {
        private readonly IServiceProvider _provider;

        public TDEEFormulaResolver(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IFormulaStrategy Resolve(string input) =>
            input switch
            {
                "1" => _provider.GetRequiredService<MifflinStJeorFormula>(),
                "2" => _provider.GetRequiredService<HarrisBenedictFormula>(),
                _ => throw new ArgumentException("Invalid formula selection")
            };
    }
}
