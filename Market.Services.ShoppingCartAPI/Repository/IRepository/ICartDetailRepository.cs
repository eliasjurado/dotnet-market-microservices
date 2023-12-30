using Market.Domain.Models;
using System.Linq.Expressions;

namespace Market.Services.CartAPI.Repository.IRepository
{
    public interface ICartDetailRepository
    {
        Task<ICollection<CartDetail>> GetAsync(Expression<Func<CartDetail, bool>> expression);
        Task<ICollection<CartDetail>> GetAsync();
        Task<CartDetail> GetAsync(long headerId, long detailId);
        Task CreateAsync(CartDetail detail);
        Task UpdateAsync(CartDetail detail);
        Task RemoveAsync(long headerId, long detailId);
        Task SaveAsync();
    }
}
