using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace jmH60A01.Models;

public partial class H60assignmentDbJmContext : DbContext
{
    public H60assignmentDbJmContext()
    {
    }

    public H60assignmentDbJmContext(DbContextOptions<H60assignmentDbJmContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=cssql; Database = H60AssignmentDB_jm; User id = JMORENCY; Password = password; TrustServerCertificate=true;;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.HasIndex(e => e.ProdCatId, "IX_Product_ProdCatId");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.BuyPrice).HasColumnType("numeric(8, 2)");
            entity.Property(e => e.Description)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.SellPrice).HasColumnType("numeric(8, 2)");

            entity.HasOne(d => d.ProdCat).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProdCatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_ProductCategory");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.ToTable("ProductCategory");
            
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.ProdCat)
                .HasMaxLength(60)
                .IsUnicode(false);
        });
        
        // Part B Seeding

        modelBuilder.Entity<ShoppingCart>(entity =>
        {
            entity.HasKey(e => e.CartId);
        });
        
        modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                CustomerId = 1, FirstName = "Bruce", LastName = "Wayne", Email = "bwayne@waynetech.com",
                PhoneNumber = "555-555-5555", Province = "QC", CreditCard = "1234-5678-1234-5678"
            },
            new Customer
            {
                CustomerId = 2, FirstName = "Jim", LastName = "Gordon", Email = "jgordon@gcpd.gov",
                PhoneNumber = "555-555-5555", Province = "ON", CreditCard = "9876-5432-9876-5432"
            },
            new Customer
            {
                CustomerId = 3, FirstName = "Harvey", LastName = "Dent", Email = "hdent@headsortails.com",
                PhoneNumber = "555-555-5555", Province = "QC", CreditCard = "1234-5678-1234-5678"
            }
        );

        modelBuilder.Entity<Order>().HasData(
            new Order
            {
                OrderId = 1, CustomerId = 1, DateCreated = DateTime.Now, DateFulfilled = DateTime.Now, Total = 100.00m,
                Taxes = 15.00m
            }
        );

        modelBuilder.Entity<OrderItem>().HasData(
            new OrderItem
            {
                OrderItemId = 1, OrderId = 1, ProductId = 3, Quantity = 1, Price = 3.50m
            },
            new OrderItem
            {
                OrderItemId = 2, OrderId = 1, ProductId = 7, Quantity = 1, Price = 4.50m
            },
            new OrderItem
            {
                OrderItemId = 3, OrderId = 1, ProductId = 12, Quantity = 1, Price = 2.50m
            }
        );
        
        modelBuilder.Entity<ShoppingCart>().HasData(
            new ShoppingCart
            {
                CartId = 1, CustomerId = 2, DateCreated = DateTime.Now
            }
        );

        modelBuilder.Entity<CartItem>().HasData(
            new CartItem
            {
                CartItemId = 1, CartId = 1, ProductId = 21, Quantity = 2, Price = 30.00m
            },
            new CartItem
            {
                CartItemId = 2, CartId = 1, ProductId = 16, Quantity = 1, Price = 9.00m
            }
        );
        
        base.OnModelCreating(modelBuilder);
    }
    
}
