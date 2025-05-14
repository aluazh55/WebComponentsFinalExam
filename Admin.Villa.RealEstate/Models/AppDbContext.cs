using Microsoft.EntityFrameworkCore;

namespace Admin.Villa.RealEstate.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options) { }

        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring relations and constraints
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.JobPosition)
                .WithMany()
                .HasForeignKey(e => e.JobPositionId)
                .OnDelete(DeleteBehavior.Restrict); // Optional: don't delete employee when position is deleted

            base.OnModelCreating(modelBuilder);
        }
    }
}
