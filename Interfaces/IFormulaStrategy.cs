namespace WeightTracker.Interfaces
{
    public interface IFormulaStrategy
    {
        double CalculateBMR(double weightKg, double heightCm, int ageYears, string sex);
        string Name { get; }
    }
}
