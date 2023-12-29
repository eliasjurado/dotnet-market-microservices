namespace Market.Domain.Models
{
    public class CartHeaderDto
    {
        public long CartHeaderId { get; set; }
        public long CouponId { get; set; }
        public string CouponCode { get; set; }
        public double CouponDisccountAmount { get; set; }
        public double CouponMinAmmount { get; set; }
        public DateTime CouponStartDate { get; set; }
        public DateTime CouponEndDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
