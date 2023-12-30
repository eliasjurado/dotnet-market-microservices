using Market.Domain.Models;
using System.Linq.Expressions;

namespace Market.Services.CartAPI.Repository.IRepository
{
    public interface ICartHeaderRepository
    {
        Task<ICollection<CartHeader>> GetAsync(Expression<Func<CartHeader, bool>> expression);
        Task<ICollection<CartHeader>> GetAsync();
        Task<CartHeader> GetAsync(long headerId);
        Task CreateAsync(CartHeader header);
        Task UpdateAsync(CartHeader header);
        Task RemoveAsync(long headerId);
        Task SaveAsync();
    }
}
