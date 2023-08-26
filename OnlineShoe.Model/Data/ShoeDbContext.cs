using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoe.Model.Data
{
    public class ShoeDbContext:IdentityDbContext<AppUser>
    {
        public ShoeDbContext(DbContextOptions<ShoeDbContext> options):base(options)
        {
                
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_item> OrderItems { get; set; }
        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Shoe_Category> Shoes_Category { get; set; }
        public DbSet<Shoe_Review> Shoe_Reviews { get; set; }    

    }
}
