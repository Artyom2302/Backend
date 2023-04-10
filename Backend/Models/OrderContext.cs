using Microsoft.EntityFrameworkCore;
using Backend.Models;
using System.Diagnostics.Metrics;

namespace Backend.Models
{
    public class OrderContext:DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options)
          : base(options)
        {
           Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
     
            
        }
        public DbSet<Lab> Labs{ get; set; }
        public DbSet<Programmer> Programmers{get; set; }
        public DbSet<User> Users{get; set; }
        public DbSet<Review> Reviews{ get; set; }
        public DbSet<StackNames> StackNames{ get; set; }
                                                                                      
    }
}
