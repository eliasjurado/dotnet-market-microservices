namespace Market.Domain.Models.Dto.Services.Cart
{
    public class CartHeaderDto
    {
        public long CartHeaderId { get; set; }
        public long CouponId { get; set; }
        public string CouponCode { get; set; }
        public double CouponDisccountAmount { get; set; }
        public double CartTotalDisccount { get; set; }
        //public double CouponMinAmmount { get; set; }
        public double CartTotal { get; set; }
        //public DateTime CouponStartDate { get; set; }
        //public DateTime CouponEndDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
