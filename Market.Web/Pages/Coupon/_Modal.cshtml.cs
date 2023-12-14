using Market.Web.Models.Dto;
using Market.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Market.Web.Pages.Coupon
{
    public class ModalModel : PageModel
    {
        private readonly ICouponService _couponService;
        public CouponDto RequestDto { get; set; }
        public ModalModel(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [BindProperty]
        public CouponDto Coupon { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _couponService.CreateAsync(Coupon);

            return RedirectToPage("/Index");
        }
    }
}