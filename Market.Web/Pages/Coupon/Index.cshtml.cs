using Htmx;
using Market.Web.Models;
using Market.Web.Models.Dto;
using Market.Web.Service.IService;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Market.Web.Pages.Coupon
{
    public class IndexModel : PageModel
    {
        private readonly ICouponService _couponService;
        public ICollection<CouponViewModel> Results { get; set; }
        public CouponDto RequestDto { get; set; }
        public IndexModel(ICouponService couponService)
        {
            _couponService = couponService;

            JArray jsonResponse = JArray.Parse(JsonConvert.SerializeObject(_couponService.GetAsync().GetAwaiter().GetResult().Data));

            Results = new List<CouponViewModel>();

            foreach (var item in jsonResponse)
            {
                JObject coupon = (JObject)item;
                Results.Add(JsonConvert.DeserializeObject<CouponViewModel>(coupon.ToString()));
            }

            RequestDto = new CouponDto();
        }

        [BindProperty(SupportsGet = true)]
        public string? Query { get; set; }

        public IActionResult OnGet()
        {
            Results = string.IsNullOrEmpty(Query)
                ? Results
                : Results.Where(g => g.ToString().Contains(Query, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!Request.IsHtmx())
                return Page();

            Response.Htmx(h =>
            {
                // we want to push the current url 
                // into the history
                h.Push(Request.GetEncodedUrl());
            });

            return Partial("_Results", this);
        }

        public IActionResult OnGetModal()
        {
            //Results = string.IsNullOrEmpty(Query)
            //    ? Results
            //    : Results.Where(g => g.ToString().Contains(Query, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!Request.IsHtmx())
                return Page();

            Response.Htmx(h =>
            {
                // we want to push the current url 
                // into the history
                h.PushUrl(Request.GetEncodedUrl());
            });

            return Partial("_Modal", this);
        }

        public IActionResult OnPost()//[FromForm] NewsletterSignup signup)
        {
            // Note: You might want more validation
            //if (!ModelState.IsValid)
            //{
            // oh no, refresh the page
            //Response.Htmx(h => h.Refresh());
            //return Content("", "text/html");
            //}

            return Partial("_Modal");//, signup);
        }

    }
}
