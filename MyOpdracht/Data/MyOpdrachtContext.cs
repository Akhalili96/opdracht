using Microsoft.EntityFrameworkCore;
using MyOpdracht.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOpdracht.Data
{
    public class MyOpdrachtContext : DbContext
    {
        public MyOpdrachtContext(DbContextOptions<MyOpdrachtContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryToProduct> CategoryToProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Item> Items { get; set; }

        public DbSet<Users> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryToProduct>()
                .HasKey(t => new { t.ProductId, t.CategoryId });

            
            modelBuilder.Entity<Item>(i =>
            {
                i.HasKey(w => w.Id);
            });




            #region Seed Data Category

            modelBuilder.Entity<Category>().HasData(new Category()
            {
                Id = 1,
                Name = "TV",
                Description = "TV"
            },
            new Category()
            {
                Id = 2,
                Name = "Audio",
                Description = "Audio"

            },
            new Category()
            {
                Id = 3,
                Name = "huishouden",
                Description = "huishouden"

            },
            new Category()
            {
                Id = 4,
                Name = "Computer",
                Description = "Computer"

            });

            modelBuilder.Entity<Item>().HasData(new Item()
            {
                Id = 1,
                Price = 3000,
                QuantityInStock = 5
            },
            new Item()
            {
                Id = 2,
                Price = 250,
                QuantityInStock = 4
            },
            new Item()
            {
                Id = 3,
                Price = 600,
                QuantityInStock = 3
            },
            new Item()
            {
                Id = 4,
                Price = 1800,
                QuantityInStock = 2
            }
                );



            modelBuilder.Entity<Product>().HasData(new Product()
            {
                Id = 1,
                ItemId = 1,
                Name = "Sony",
                Description = "55 Inch"
            },
             new Product()
             {
                 Id = 2,
                 ItemId = 2,
                 Name = "harman kardon",
                 Description = "Onyx Studio"
             },
              new Product()
              {
                  Id = 3,
                  ItemId = 3,
                  Name = "Wasmachines",
                  Description = "WHIRLPOOL "
              },
              new Product()
              {
                  Id = 4,
                  ItemId = 4,
                  Name = "Apple",
                  Description = " MacBooks"
              }

            );


            modelBuilder.Entity<CategoryToProduct>().HasData(
                new CategoryToProduct() { CategoryId = 1, ProductId = 1 },
                new CategoryToProduct() { CategoryId = 2, ProductId = 1 },
                new CategoryToProduct() { CategoryId = 3, ProductId = 1 },
                new CategoryToProduct() { CategoryId = 4, ProductId = 1 },
                new CategoryToProduct() { CategoryId = 1, ProductId = 2 },
                new CategoryToProduct() { CategoryId = 2, ProductId = 2 },
                new CategoryToProduct() { CategoryId = 3, ProductId = 2 },
                new CategoryToProduct() { CategoryId = 4, ProductId = 2 },
                new CategoryToProduct() { CategoryId = 1, ProductId = 3 },
                new CategoryToProduct() { CategoryId = 2, ProductId = 3 },
                new CategoryToProduct() { CategoryId = 3, ProductId = 3 },
                new CategoryToProduct() { CategoryId = 4, ProductId = 3 },
                new CategoryToProduct() { CategoryId = 1, ProductId = 4 },
                new CategoryToProduct() { CategoryId = 2, ProductId = 4 },
                new CategoryToProduct() { CategoryId = 3, ProductId = 4 },
                new CategoryToProduct() { CategoryId = 4, ProductId = 4 }
                );

            #endregion
            base.OnModelCreating(modelBuilder);

        }
    }
}
