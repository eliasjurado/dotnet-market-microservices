using Market.Web.Models.Dto;

namespace Market.Web.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseInterface> SendAsync(RequestInterface request);
    }
}
