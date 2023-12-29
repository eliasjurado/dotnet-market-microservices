
using Market.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Services.CouponAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().ToTable("Coupon").HasData(
                new Coupon
                {
                    CouponId = 1,
                    CouponCode = "50OFF",
                    Name = "50% OFF - New Year Disccount",
                    Description = "Special Disccount for New Year",
                    CouponDisccountAmount = 50,
                    CouponMinAmmount = 1200,
                    CouponStartDate = new DateTime(2023, 12, 29),
                    CouponEndDate = new DateTime(2024, 1, 15)
                }
            );
            modelBuilder.Entity<Coupon>().ToTable("Coupon").HasData(
                new Coupon
                {
                    CouponId = 2,
                    CouponCode = "15OFF",
                    Name = "15% OFF - Carnival Disccount",
                    Description = "Special Disccount for Carnival",
                    CouponDisccountAmount = 15,
                    CouponMinAmmount = 200,
                    CouponStartDate = new DateTime(2024, 2, 1),
                    CouponEndDate = new DateTime(2024, 2, 29)
                }
            );
        }
    }
}
