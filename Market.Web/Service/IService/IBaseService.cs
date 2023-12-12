using Market.Web.Models.Dto;

namespace Market.Web.Service.IService
{
    public interface IBaseService<T> where T : class
    {
        Task<ResponseDto<T?>> SendAsync(RequestDto<T> request);

    }
}
