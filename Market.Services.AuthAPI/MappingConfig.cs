using AutoMapper;
using Market.Domain.Models.Dto.Services.Auth;
using Market.Services.AuthAPI.Models;

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
