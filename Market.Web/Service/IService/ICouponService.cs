
using Market.Web.Models.Dto;

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
