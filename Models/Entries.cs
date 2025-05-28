namespace WeightTracker.Models
{
    public class Entries
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Weight { get; set; }
        public int TDEE { get; set; }

        public virtual Users? Users { get; set; }
    }
}
