using Microsoft.EntityFrameworkCore;

namespace Homework7_Solution.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
    {

    }

    // Needed for Many-to-Many association entity
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductOrder>().HasKey(s => new {s.OrderID, s.ProductID});
    }

    public DbSet<Order> Orders {get; set;}
    public DbSet<Product> Products {get; set;}
    public DbSet<ProductOrder> ProductOrders {get; set;}
}