using WeightTracker.Interfaces;

namespace WeightTracker.Services
{
    public class MifflinStJeorFormula : IFormulaStrategy
    {
        public string Name => "Mifflin-St Jeor";

        public double CalculateBMR(double weightKg, double heightCm, int ageYears, string sex)
        {
            return sex.ToUpper() == "M"
                ? (10 * weightKg) + (6.25 * heightCm) - (5 * ageYears) + 5
                : (10 * weightKg) + (6.25 * heightCm) - (5 * ageYears) - 161;
        }
    }
}
