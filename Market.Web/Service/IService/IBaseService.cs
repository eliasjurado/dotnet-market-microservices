using Market.Web.Models.Dto;

namespace Market.Web.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDto> SendAsync(RequestDto request);
    }
}
