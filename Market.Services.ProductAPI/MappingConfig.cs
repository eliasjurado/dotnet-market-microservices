using AutoMapper;
using Market.Domain.Models;
using Market.Domain.Models.Dto.Services.Product;

namespace Market.Services.ProductAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
