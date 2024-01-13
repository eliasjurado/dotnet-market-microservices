using FluentValidation;
using Market.Domain.Models.Dto.Services.Cart;

namespace Market.Services.CartAPI.Validations
{
    public class CartRequestValidation : AbstractValidator<CartDto>
    {
        public CartRequestValidation()
        {
            //RuleFor(model => model.CouponCode).NotEmpty();
            //RuleFor(model => model.Name).NotEmpty();
            //RuleFor(model => model.CouponDisccountAmount).ExclusiveBetween(0, 100);
            //RuleFor(model => model.CouponMinAmmount).GreaterThanOrEqualTo(0);
        }
    }
}
