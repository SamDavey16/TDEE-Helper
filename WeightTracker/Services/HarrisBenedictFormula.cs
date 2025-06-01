using WeightTracker.Interfaces;

namespace WeightTracker.Services
{
    public class HarrisBenedictFormula : IFormulaStrategy
    {
        public string Name => "Harris-Benedict";

        public double CalculateBMR(double weightKg, double heightCm, int ageYears, string sex)
        {
            return sex.ToUpper() == "M"
                ? 66.47 + (13.75 * weightKg) + (5.003 * heightCm) - (6.755 * ageYears)
                : 655.1 + (9.563 * weightKg) + (1.850 * heightCm) - (4.676 * ageYears);
        }
    }
}
