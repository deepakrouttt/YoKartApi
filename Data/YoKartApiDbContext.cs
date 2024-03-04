using Microsoft.EntityFrameworkCore;
using YoKartApi.Models;

namespace YoKartApi.Data
{
    public class YoKartApiDbContext : DbContext
    {


        public YoKartApiDbContext(DbContextOptions<YoKartApiDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}

