using Market.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Services.CartAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CartHeader>().ToTable("CartHeader");
            modelBuilder.Entity<CartDetail>().ToTable("CartDetail").HasKey(x => new { x.CartHeaderId, x.CartDetailId }); ;
        }
    }
}
