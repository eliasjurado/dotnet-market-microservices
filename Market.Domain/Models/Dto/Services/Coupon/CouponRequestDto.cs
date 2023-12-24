using System.Text.Json.Serialization;

namespace Market.Domain.Models.Dto.Services.Coupon
{
    public class CouponRequestDto : CouponDto
    {
        [JsonIgnore]
        public override int CouponId { get; set; }
    }
}
