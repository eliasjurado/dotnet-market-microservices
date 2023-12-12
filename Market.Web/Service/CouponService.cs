using Market.Web.Models.Dto;
using Market.Web.Service.IService;

namespace Market.Web.Service
{
    public class CouponService : IService<CouponDto>
    {
        private readonly IBaseService<CouponDto> _baseService;
        public CouponService(IBaseService<CouponDto> baseService)
        {
            _baseService = baseService;
        }

        public Task<ResponseDto<CouponDto>> CreateAsync(CouponDto coupon)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<ICollection<CouponDto>>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<CouponDto>> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<CouponDto>> GetAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<CouponDto>> RemoveAsync(CouponDto coupon)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<CouponDto>> UpdateAsync(CouponDto coupon)
        {
            throw new NotImplementedException();
        }
    }
}
