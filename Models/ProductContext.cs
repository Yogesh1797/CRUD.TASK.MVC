using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CRUD.TASK.MVC.Models
{
    public class ProductContext : DbContext
    {

        public ProductContext()
           : base("ProductContext")
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasKey(p => p.CategoryId);
            modelBuilder.Entity<Category>().Property(c => c.CategoryId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Product>().HasKey(b => b.ProductId);
            modelBuilder.Entity<Product>().Property(b => b.ProductId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Product>().HasRequired(p => p.Category)
                .WithMany(b => b.Products).HasForeignKey(b => b.CategoryId);

            base.OnModelCreating(modelBuilder);
        }
    }
}