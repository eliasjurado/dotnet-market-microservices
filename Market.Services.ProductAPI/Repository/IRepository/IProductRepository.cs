using Market.Domain.Models;
using System.Linq.Expressions;

namespace Market.Services.ProductAPI.Repository.IRepository
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> GetAsync(Expression<Func<Product, bool>> expression);
        Task<ICollection<Product>> GetAsync();
        Task<Product> GetAsync(long productId);
        Task<Product> GetAsync(string productName);
        Task CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task RemoveAsync(long productId);
        Task SaveAsync();
    }
}
