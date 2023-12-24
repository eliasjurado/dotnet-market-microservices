using FluentValidation;
using Market.Domain.Models.Dto.Services.Coupon;

namespace Market.Services.CouponAPI.Validations
{
    public class CouponRequestValidation : AbstractValidator<CouponRequestDto>
    {
        public CouponRequestValidation()
        {
            RuleFor(model => model.CouponCode).NotEmpty();
            RuleFor(model => model.CouponName).NotEmpty();
            RuleFor(model => model.CouponDisccountAmount).ExclusiveBetween(0, 100);
            RuleFor(model => model.CouponMinAmmount).GreaterThanOrEqualTo(0);
        }
    }
}
