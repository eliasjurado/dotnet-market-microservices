using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Models
{
    public class Coupon : BaseModel
    {
        [Key]
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public string CouponName { get; set; }
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
