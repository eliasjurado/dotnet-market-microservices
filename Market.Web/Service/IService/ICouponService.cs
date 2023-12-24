
using Market.Domain.Models.Dto.Services.Coupon;
using Market.Domain.Models.Dto.Web;

namespace Market.Web.Service.IService
{
    public interface ICouponService
    {
        Task<ResponseInterface?> GetAsync();
        Task<ResponseInterface?> GetAsync(int id);
        Task<ResponseInterface?> GetAsync(string code);
        Task<ResponseInterface?> CreateAsync(CouponDto requestDto);
        Task<ResponseInterface?> UpdateAsync(CouponDto requestDto);
        Task<ResponseInterface?> RemoveAsync(int id);
    }
}
