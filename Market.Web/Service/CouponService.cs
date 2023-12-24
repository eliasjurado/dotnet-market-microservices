using Market.Web.Models.Dto;
using Market.Web.Service.IService;
using static Market.Infrastructure.Base;

namespace Market.Web.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseInterface?> CreateAsync(CouponDto requestDto)
        {
            var request = new RequestInterface
            {
                ApiType = ApiType.POST,
                Url = CouponAPIBase + "/api/coupon",
                Data = requestDto
            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseInterface?> GetAsync()
        {
            var request = new RequestInterface
            {
                ApiType = ApiType.GET,
                Url = CouponAPIBase + "/api/coupon"
            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseInterface?> GetAsync(int id)
        {
            var request = new RequestInterface
            {
                ApiType = ApiType.GET,
                Url = CouponAPIBase + "/api/coupon/" + id
            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseInterface?> GetAsync(string code)
        {
            var request = new RequestInterface
            {
                ApiType = ApiType.GET,
                Url = CouponAPIBase + "/api/coupon/" + code
            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseInterface?> RemoveAsync(int id)
        {
            var request = new RequestInterface
            {
                ApiType = ApiType.DELETE,
                Url = CouponAPIBase + "/api/coupon/" + id
            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseInterface?> UpdateAsync(CouponDto requestDto)
        {
            var request = new RequestInterface
            {
                ApiType = ApiType.PUT,
                Url = CouponAPIBase + "/api/coupon/" + requestDto.CouponId,
                Data = requestDto
            };
            return await _baseService.SendAsync(request);
        }

    }
}
