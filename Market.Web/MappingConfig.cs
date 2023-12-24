using AutoMapper;
using Market.Web.Models;
using Market.Web.Models.Dto;

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
