using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Domain.Models
{
    public class CartHeader : BaseModel
    {
        [Key]
        public long CartHeaderId { get; set; }
        public long CouponId { get; set; }
        [NotMapped]
        public string CouponCode { get; set; }
        [NotMapped]
        public double CouponDisccountAmount { get; set; }
        [NotMapped]
        public double CouponMinAmmount { get; set; }
        [NotMapped]
        public DateTime CouponStartDate { get; set; }
        [NotMapped]
        public DateTime CouponEndDate { get; set; }
    }
}
