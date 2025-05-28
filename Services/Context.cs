using Microsoft.EntityFrameworkCore;
using WeightTracker.Models;

namespace WeightTracker.Services
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<Entries> Entries { get; set; }
        public DbSet<Users> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entries>(entity =>
            {
                entity.ToTable("Entries");
                entity.HasKey(e => e.Id);
                entity.Property(entity => entity.Id).HasColumnName("Id").IsRequired();
                entity.Property(entity => entity.UserId).HasColumnName("UserId").IsRequired();
                entity.Property(entity => entity.Weight).HasColumnName("Weight").IsRequired();
                entity.Property(entity => entity.TDEE).HasColumnName("TDEE").IsRequired();
                entity.HasOne(entity => entity.Users).WithMany(entity  => entity.Entries).HasForeignKey(entity =>  entity.UserId);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Id).HasColumnName("UserId").IsRequired();
                entity.Property(entity => entity.Name).HasColumnName("Name");
            });
        }
    }
}
