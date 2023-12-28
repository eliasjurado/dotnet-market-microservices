using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Models
{
    public sealed class Coupon : BaseModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(10)]
        public string Code { get; set; }
        [Required]
        [Range(0, 100)]
        public double DisccountAmount { get; set; } = 0;
        [Required]
        public double MinAmmount { get; set; } = 0;
        [Required]
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        [Required]
        public DateTime EndDate { get; set; } = DateTime.MaxValue;
    }
}
