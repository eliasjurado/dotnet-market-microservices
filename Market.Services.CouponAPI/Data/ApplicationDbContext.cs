
using Market.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Services.CouponAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //public DbSet<User> Users { get; set; }
        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().ToTable("LocalUser");
            modelBuilder.Entity<Coupon>().ToTable("Coupon").HasData(
                new Coupon
                {
                    CouponId = 1,
                    CouponCode = "15OFF",
                    CouponName = "15% OFF - Christmas Disccount",
                    CouponDisccountAmount = 15,
                    CouponMinAmmount = 200,
                });
        }

    }
}
