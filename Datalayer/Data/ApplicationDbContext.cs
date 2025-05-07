using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Bluetooth Speaker",
                Description = "Portable and powerful.",
                Category = "Electronics",
                ProductImageUrl = "https://via.placeholder.com/200",
                Quantity = 10,
                Brand = "Sony",
                Price = 15000,
                UnitCost = 11000,
                CreatedAt = DateTime.UtcNow
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Smart Watch",
                Description = "Waterproof and stylish.",
                Category = "Wearables",
                ProductImageUrl = "https://via.placeholder.com/200",
                Quantity = 20,
                Brand = "Samsung",
                Price = 25000,
                UnitCost = 18000,
                CreatedAt = DateTime.UtcNow
            });
            // Define composite primary key using HasKey

            modelBuilder.Entity<Product>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Cart>()
              .HasKey(c => c.Id);
            modelBuilder.Entity<CartItem>()
                .HasKey(c => c.Id);
        }

       public DbSet<Product> Products { get; set; }
       public DbSet<Cart> Carts { get; set; }
       public DbSet<CartItem> CartItems { get; set; }
    }

    }
