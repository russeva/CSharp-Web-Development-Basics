namespace WebServerV._2.ByTheCakeApplication.Data
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using ByTheCakeApplication.Data.Models;

    public class ByTheCakeDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orderd { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<OrderProduct> OrderProduct { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=.;Database=ByTheCakeDb;Integrated Security=true");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OrderProduct>()
                .HasKey(k => new { k.OrderId, k.ProductId});

            builder.Entity<User>()
                .HasMany(o => o.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);

            builder.Entity<Product>()
                .HasMany(p => p.Orders)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId);

            builder.Entity<Order>()
                .HasMany(o => o.Products)
                .WithOne(o => o.Order)
                .HasForeignKey(o => o.OrderId);
        }
    }
}
