using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoe.Model.Data
{
    public class ShoeDbContext : IdentityDbContext<AppUser>
    {
        public ShoeDbContext(DbContextOptions<ShoeDbContext> options) : base(options)
        {

        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_item> OrderItems { get; set; }
        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Shoe_Review> Shoe_Reviews { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
          
            modelBuilder.Entity<Shoe_Review>(entity => {

                entity.HasKey(s => s.Id);
                entity.Property(s => s.Comment).IsRequired();

                entity.HasOne(c => c.Shoe)
              .WithMany(sc => sc.Reviews)
              .HasForeignKey(c => c.Shoe_Id)
              .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Order_item>(entity => {

                entity.HasKey(s => s.Id);
                
               entity.HasOne(c => c.Shoe)
              .WithMany(sc => sc.Order_Items)
              .HasForeignKey(c => c.Shoe_Id)
              .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(c => c.Order)
            .WithMany(sc => sc.order_Items)
            .HasForeignKey(c => c.Order_Id)
            .OnDelete(DeleteBehavior.NoAction);
            });
         
        }
    } 
}
