using Market.Domain.Models.Dto.Services.Product;
using Market.Domain.Models.Dto.Web;
using Market.Web.Service.IService;
using static Market.Infrastructure.Base;

namespace Market.Web.Service
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;
        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseInterface?> CreateAsync(ProductDto requestDto)
        {
            var request = new RequestInterface
            {
                ApiType = ApiType.POST,
                Url = ProductAPIBase + "/api/product",
                Data = requestDto
            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseInterface?> GetAsync()
        {
            var request = new RequestInterface
            {
                ApiType = ApiType.GET,
                Url = ProductAPIBase + "/api/product"
            };
            return await _baseService.SendAsync(request, false);
        }

        public async Task<ResponseInterface?> GetCategoriesAsync()
        {
            var request = new RequestInterface
            {
                ApiType = ApiType.GET,
                Url = ProductAPIBase + "/api/category"
            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseInterface?> GetAsync(int id)
        {
            var request = new RequestInterface
            {
                ApiType = ApiType.GET,
                Url = ProductAPIBase + "/api/product/" + id
            };
            return await _baseService.SendAsync(request, false);
        }

        public async Task<ResponseInterface?> GetAsync(string code)
        {
            var request = new RequestInterface
            {
                ApiType = ApiType.GET,
                Url = ProductAPIBase + "/api/product/" + code
            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseInterface?> RemoveAsync(int id)
        {
            var request = new RequestInterface
            {
                ApiType = ApiType.DELETE,
                Url = ProductAPIBase + "/api/product/" + id
            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseInterface?> UpdateAsync(ProductDto requestDto)
        {
            var request = new RequestInterface
            {
                ApiType = ApiType.PUT,
                Url = ProductAPIBase + "/api/product/" + requestDto.Id,
                Data = requestDto
            };
            return await _baseService.SendAsync(request);
        }

    }
}
