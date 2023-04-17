using Microsoft.EntityFrameworkCore;
using Backend.Models;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;

namespace Backend.Models
{
    public class OrderContext:DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options)
          : base(options)
        {
           //Database.EnsureDeleted();
           Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Programmer>()
                .HasMany(p => p.Labs)
                .WithOne()
                .HasForeignKey(l => l.ProgrammerId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<User>()
                .HasMany(p => p.Labs)
                .WithOne()
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<StackName>().HasAlternateKey(s => s.Name);
        }
        public DbSet<Lab> Labs{ get; set; }
        public DbSet<Programmer> Programmers{get; set; }
        public DbSet<User> Users{get; set; }
        public DbSet<Review> Reviews{ get; set; }
        public DbSet<StackNames> StackNames{ get; set; }
        public DbSet<StackName> StackName { get; set; }

    }
}
