using AutoMapper;
using Market.Domain.Models.Dto.Services.Auth;
using Market.Domain.Models.Dto.Services.Coupon;
using Market.Domain.Models.Dto.Web;

namespace Market.Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<CouponDto, CouponViewModel>().ReverseMap();
            CreateMap<RoleRequestDto, SignUpRequestDto>().ReverseMap();
        }
    }
}
