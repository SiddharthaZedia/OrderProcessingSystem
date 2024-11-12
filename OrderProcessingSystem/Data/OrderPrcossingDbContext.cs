using Microsoft.EntityFrameworkCore;
using OrderProcessingSystem.Model.Domain;

namespace OrderProcessingSystem.Repositories
{
    public class OrderPrcossingDbContext : DbContext
    {
        public OrderPrcossingDbContext(DbContextOptions<OrderPrcossingDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.ProductId });
        }
    }

}
