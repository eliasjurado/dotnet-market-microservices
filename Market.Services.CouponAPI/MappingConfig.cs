using AutoMapper;
using Market.Domain.Models;
using Market.Domain.Models.Dto.Services.Coupon;

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
