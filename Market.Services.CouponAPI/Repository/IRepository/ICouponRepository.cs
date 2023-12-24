using Market.Domain.Models;
using System.Linq.Expressions;

namespace Market.Services.CouponAPI.Repository.IRepository
{
    public interface ICouponRepository
    {
        Task<ICollection<Coupon>> GetAsync(Expression<Func<Coupon, bool>> expression);
        Task<ICollection<Coupon>> GetAsync();
        Task<Coupon> GetAsync(int id);
        Task<Coupon> GetAsync(string name);
        Task CreateAsync(Coupon coupon);
        Task UpdateAsync(Coupon coupon);
        Task RemoveAsync(Coupon coupon);
        Task SaveAsync();
    }
}
