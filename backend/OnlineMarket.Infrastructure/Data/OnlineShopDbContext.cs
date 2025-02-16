using Microsoft.EntityFrameworkCore;
using OnlineMarket.Domain.Entities;

namespace OnlineMarket.Infrastructure.Data;

public class OnlineShopDbContext(DbContextOptions<OnlineShopDbContext> options) : DbContext(options)
{
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<OrderProduct> OrderProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>()
            .ToTable("Orders");

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(o => o.CustomerFullName)
                .HasMaxLength(50);

            entity.Property(o => o.CustomerPhone)
                .HasMaxLength(13);
        });

        modelBuilder.Entity<Product>()
            .ToTable("Products");

        modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Price)
                    .HasColumnType("decimal(18,2)");

                entity.Property(p => p.Name)
                    .HasMaxLength(30);
            });

        modelBuilder.Entity<OrderProduct>()
            .HasKey(op => op.Id);

        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Order)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(op => op.OrderId);

        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Product)
            .WithMany(p => p.OrderProducts)
            .HasForeignKey(op => op.ProductId);
    }
}