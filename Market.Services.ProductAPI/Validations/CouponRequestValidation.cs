using FluentValidation;
using Market.Domain.Models.Dto.Services.Product;

namespace Market.Services.ProductAPI.Validations
{
    public class ProductRequestValidation : AbstractValidator<ProductRequestDto>
    {
        public ProductRequestValidation()
        {
            RuleFor(model => model.Name).NotEmpty();
        }
    }
}
