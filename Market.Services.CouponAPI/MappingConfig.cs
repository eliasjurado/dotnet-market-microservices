using AutoMapper;
using Market.Services.CouponAPI.Models;
using Market.Services.CouponAPI.Models.DTO;

namespace Market.Services.CouponAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Coupon, CouponRequestDto>().ReverseMap();
            CreateMap<Coupon, CouponDto>().ReverseMap();
        }
    }
}
