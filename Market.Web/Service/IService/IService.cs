using Market.Web.Models.Dto;

namespace Market.Web.Service.IService
{
    public interface IService<T> where T : class
    {
        Task<ResponseDto<ICollection<T>>> GetAsync();
        Task<ResponseDto<T>> GetAsync(int id);
        Task<ResponseDto<T>> GetAsync(string code);
        Task<ResponseDto<T>> CreateAsync(T coupon);
        Task<ResponseDto<T>> UpdateAsync(T coupon);
        Task<ResponseDto<T>> RemoveAsync(T coupon);
    }
}
