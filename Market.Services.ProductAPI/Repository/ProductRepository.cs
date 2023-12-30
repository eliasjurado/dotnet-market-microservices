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

        public async Task CreateAsync(Product product)
        {
            await Task.Run(() => _db.Add(product));
        }

        public async Task<Product> GetAsync(long productId)
        {
            return await _db.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
        }

        public async Task<ICollection<Product>> GetAsync(Expression<Func<Product, bool>> expression)
        {
            return await _db.Products.Where(expression).ToListAsync();
        }

        public async Task<ICollection<Product>> GetAsync()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task RemoveAsync(long productId)
        {
            await Task.Run(async () => _db.Remove(await GetAsync(productId)));
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            await Task.Run(() => _db.Products.Update(product));
        }
    }
}
