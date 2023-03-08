using Microsoft.EntityFrameworkCore;
namespace Backend.Models
{
    public class DayContext: DbContext
    {
        public DayContext(DbContextOptions<DayContext> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Day> Days { get; set; }
        public DbSet<Activity> Activities{ get; set; }
    }
}
