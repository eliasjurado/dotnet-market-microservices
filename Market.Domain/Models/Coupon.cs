using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Models
{
    public sealed class Coupon : BaseModel
    {
        [Key]
        public long CouponId { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(10)]
        public string CouponCode { get; set; }
        [Required]
        [Range(0, 100)]
        public double CouponDisccountAmount { get; set; } = 0;
        [Required]
        public double CouponMinAmmount { get; set; } = 0;
        [Required]
        public DateTime CouponStartDate { get; set; } = DateTime.MinValue;
        [Required]
        public DateTime CouponEndDate { get; set; } = DateTime.MaxValue;
    }
}
