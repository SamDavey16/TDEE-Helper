using WeightTracker.Interfaces;

namespace WeightTracker.Services
{
    public class TDEECalculator
    {
        private readonly IFormulaStrategy _formula;
        private readonly IActivityLevelStrategy _activity;

        public TDEECalculator(IFormulaStrategy formula, IActivityLevelStrategy activity)
        {
            _formula = formula;
            _activity = activity;
        }

        public double CalculateTDEE(double weight, double height, int age, string sex)
        {
            var bmr = _formula.CalculateBMR(weight, height, age, sex);
            return bmr * _activity.GetActivityMultiplier();
        }
    }
}
