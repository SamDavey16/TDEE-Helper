namespace WeightTracker.Models
{
    public class Entries
    {
        public string FormulaChoice { get; set; }
        public string ActivityChoice { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public int TDEE { get; set; }

        public virtual Users? Users { get; set; }
    }
}
