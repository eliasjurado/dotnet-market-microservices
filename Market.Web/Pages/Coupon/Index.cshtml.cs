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
        [BindProperty(SupportsGet = true)]
        public CouponDto RequestDto { get; set; }
        public ResponseDto ResponseDto { get; set; }
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
        }

        [BindProperty(SupportsGet = true)]
        public string UpsertModalTitle { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Query { get; set; }

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
                h.PushUrl(Request.GetEncodedUrl());
            });

            return Partial("_Results", this);
        }

        public IActionResult OnGetCreateModal()
        {
            UpsertModalTitle = "Create";

            if (!Request.IsHtmx())
                return Page();

            Response.Htmx(h =>
            {
                h.PushUrl(Request.GetEncodedUrl());
            });

            return Partial("_UpsertModal", this);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _couponService.CreateAsync(RequestDto);

            return RedirectToPage("Index");
        }
    }
}
