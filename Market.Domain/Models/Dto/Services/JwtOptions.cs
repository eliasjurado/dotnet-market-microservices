using Market.Domain.Models.Dto.Services.Coupon;

namespace Market.Domain.Models.Dto.Services
{
    public class JwtOptions
    {
        public string Secret { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;

        new CouponRequestDto code { get; set; } = new CouponRequestDto { };
    }
}
