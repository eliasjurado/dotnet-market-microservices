﻿namespace Market.Services.CouponAPI.Models.Dto
{
    public class CouponRequestDto
    {
        public string CouponCode { get; set; }
        public string CouponName { get; set; }
        public double CouponDisccountAmount { get; set; }
        public double CouponMinAmmount { get; set; }
        public DateTime CouponStartDate { get; set; }
        public DateTime CouponEndDate { get; set; }
    }
}
