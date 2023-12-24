using AutoMapper;
using Market.Domain.Models;
using Market.Domain.Models.Dto.Services.Auth;

namespace Market.Services.CouponAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
        }
    }
}
