using FluentValidation;
using Market.Domain.Models.Dto.Services.Coupon;

namespace Market.Services.CouponAPI.Validations
{
    public class CouponRequestValidation : AbstractValidator<CouponRequestDto>
    {
        public CouponRequestValidation()
        {
            RuleFor(model => model.Code).NotEmpty();
            RuleFor(model => model.Name).NotEmpty();
            RuleFor(model => model.DisccountAmount).ExclusiveBetween(0, 100);
            RuleFor(model => model.MinAmmount).GreaterThanOrEqualTo(0);
        }
    }
}
