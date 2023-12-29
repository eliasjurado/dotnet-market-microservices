
using Market.Domain.Models.Dto.Services.Product;
using Market.Domain.Models.Dto.Web;

namespace Market.Web.Service.IService
{
    public interface IProductService
    {
        Task<ResponseInterface?> GetAsync();
        Task<ResponseInterface?> GetAsync(int id);
        Task<ResponseInterface?> CreateAsync(ProductDto requestDto);
        Task<ResponseInterface?> UpdateAsync(ProductDto requestDto);
        Task<ResponseInterface?> RemoveAsync(int id);
        Task<ResponseInterface?> GetCategoriesAsync();
    }
}
