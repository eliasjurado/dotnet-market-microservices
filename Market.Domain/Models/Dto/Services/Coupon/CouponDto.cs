namespace Market.Domain.Models.Dto.Services.Coupon
{
    public class CouponDto
    {
        public virtual long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double DisccountAmount { get; set; }
        public double MinAmmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
