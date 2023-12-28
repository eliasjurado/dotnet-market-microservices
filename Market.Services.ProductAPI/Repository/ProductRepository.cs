using Market.Domain.Models;
using Market.Services.ProductAPI.Data;
using Market.Services.ProductAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Market.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Product Product)
        {
            await Task.Run(() => _db.Add(Product));
        }

        public async Task<Product> GetAsync(int id)
        {
            return await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product> GetAsync(string name)
        {
            return await _db.Products.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        }

        public async Task<ICollection<Product>> GetAsync(Expression<Func<Product, bool>> expression)
        {
            return await _db.Products.Where(expression).ToListAsync();
        }

        public async Task<ICollection<Product>> GetAsync()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task RemoveAsync(Product Product)
        {
            await Task.Run(() => _db.Remove(Product));
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product Product)
        {
            await Task.Run(() => _db.Products.Update(Product));
        }
    }
}
