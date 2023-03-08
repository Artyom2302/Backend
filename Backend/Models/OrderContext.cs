using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class OrderContext:DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options)
          : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>().HasKey(o => new {o.Id});
            modelBuilder.Entity<Programmer>().HasKey(p => new { p.Order_Id, p.Surname,p.Name });
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Programmer> Programmers { get; set; }

    }
}
