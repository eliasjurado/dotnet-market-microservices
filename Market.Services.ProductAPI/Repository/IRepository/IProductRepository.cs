using Market.Domain.Models;
using System.Linq.Expressions;

namespace Market.Services.ProductAPI.Repository.IRepository
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> GetAsync(Expression<Func<Product, bool>> expression);
        Task<ICollection<Product>> GetAsync();
        Task<Product> GetAsync(int id);
        Task<Product> GetAsync(string name);
        Task CreateAsync(Product coupon);
        Task UpdateAsync(Product coupon);
        Task RemoveAsync(Product coupon);
        Task SaveAsync();
    }
}
