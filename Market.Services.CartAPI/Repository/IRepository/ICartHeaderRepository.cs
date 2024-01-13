using Market.Domain.Models;
using System.Linq.Expressions;

namespace Market.Services.CartAPI.Repository.IRepository
{
    public interface ICartHeaderRepository
    {
        Task<ICollection<CartHeader>> GetAsync(Expression<Func<CartHeader, bool>> expression);
        Task<ICollection<CartHeader>> GetAsync();
        Task<CartHeader> GetAsync(Guid userId);
        Task<CartHeader> GetAsync(long headerId);
        Task<CartHeader> GetAsync(Guid userId, long headerId);
        Task CreateAsync(CartHeader header);
        Task UpdateAsync(CartHeader header);
        Task RemoveAsync(long headerId);
        Task RemoveAsync(Guid userId, long headerId);
        Task SaveAsync();
    }
}
