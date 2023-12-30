using AutoMapper;
using Market.Domain.Models;
using Market.Domain.Models.Dto.Services.Cart;

namespace Market.Services.CouponAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
            CreateMap<CartDetail, CartDetailDto>().ReverseMap();
        }
    }
}
