using FluentValidation;
using Market.Services.CouponAPI.Models.Dto;

namespace Market.Services.CouponAPI.Validations
{
    public class CouponRequestValidation : AbstractValidator<CouponRequestDto>
    {
        public CouponRequestValidation()
        {
            RuleFor(model => model.CouponCode).NotEmpty();
            RuleFor(model => model.CouponName).NotEmpty();
            RuleFor(model => model.CouponDisccountAmount).InclusiveBetween(1, 100);
            RuleFor(model => model.CouponMinAmmount).GreaterThanOrEqualTo(0);
        }
    }
}
