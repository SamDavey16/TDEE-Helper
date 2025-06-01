namespace WeightTracker.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual List<Entries>? Entries { get; set; }
    }
}
