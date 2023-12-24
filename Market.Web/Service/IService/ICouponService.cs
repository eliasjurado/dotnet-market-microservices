
using Market.Web.Models.Dto;

namespace Market.Web.Service.IService
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetAsync();
        Task<ResponseDto?> GetAsync(int id);
        Task<ResponseDto?> GetAsync(string code);
        Task<ResponseDto?> CreateAsync(CouponDto requestDto);
        Task<ResponseDto?> UpdateAsync(CouponDto requestDto);
        Task<ResponseDto?> RemoveAsync(int id);
    }
}
