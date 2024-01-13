using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Domain.Models
{
    public sealed class Coupon : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
