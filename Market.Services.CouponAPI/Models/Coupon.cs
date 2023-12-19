using System.ComponentModel.DataAnnotations;
using static Market.Infrastructure.Base;

namespace Market.Services.CouponAPI.Models
{
    public class Coupon
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
        [Required]
        public ByteType IsDeleted { get; set; } = ByteType.No;
        [Required]
        public string CreatedBy { get; set; } = DefaultUser;
        [Required]
        public string UpdatedBy { get; set; } = DefaultUser;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
