using AutoMapper;
using Market.Services.AuthAPI.Models;
using Market.Services.AuthAPI.Models.Dto;

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
