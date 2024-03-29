﻿namespace Market.Domain.Models.Dto.Web
{
    public sealed record CouponViewModel
    {
        public long CouponId { get; set; }
        public string CouponCode { get; set; }
        public string Name { get; set; }
        public double CouponDisccountAmount { get; set; }
        public double CouponMinAmmount { get; set; }
        public DateTime CouponStartDate { get; set; }
        public DateTime CouponEndDate { get; set; }
    }
}
