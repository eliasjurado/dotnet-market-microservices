using Market.Domain.Models.Dto.Web;

namespace Market.Web.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseInterface> SendAsync(RequestInterface request);
    }
}
