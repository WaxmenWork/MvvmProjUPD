using Microsoft.EntityFrameworkCore;
using MvvmProjUPD.MVVM.Models;

public class MyDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<PickupPoint> PickupPoints { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(local); Database=AleksandrovichPA_demoexam; Integrated Security=true; TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("User")
            .HasKey(u => u.UserID);

        modelBuilder.Entity<Role>().ToTable("Role")
            .HasKey(r => r.RoleID);

        modelBuilder.Entity<Order>().ToTable("Order")
            .HasKey(o => o.OrderID);

        modelBuilder.Entity<OrderProduct>().ToTable("OrderProduct")
            .HasKey(op => new { op.OrderID, op.ProductArticleNumber });

        modelBuilder.Entity<PickupPoint>().ToTable("PickupPoint")
            .HasKey(pp => pp.PickupPointID);

        modelBuilder.Entity<Product>().ToTable("Product")
            .HasKey(p => p.ProductArticleNumber);
    }
}
