
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
                    Id = 1,
                    Code = "15OFF",
                    Name = "15% OFF - Christmas Disccount",
                    Description = "Special Disccount for Christmas",
                    DisccountAmount = 15,
                    MinAmmount = 200,
                });
        }

    }
}
