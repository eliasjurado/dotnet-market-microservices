﻿using Htmx;
using Market.Domain.Models.Dto.Services.Coupon;
using Market.Domain.Models.Dto.Web;
using Market.Web.Service.IService;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Market.Web.Pages.Coupon
{
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        private readonly ICouponService _couponService;
        [BindProperty(SupportsGet = true)]
        public CouponViewModel model { get; set; }
        public ICollection<CouponViewModel> Results { get; set; }
        [BindProperty(SupportsGet = true)]
        public CouponDto RequestDto { get; set; }
        public int ResultsCount { get; set; } = 0;
        public ResponseInterface ResponseDto { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ToastTitle { get; set; } = string.Empty;
        [BindProperty(SupportsGet = true)]
        public string ToastBody { get; set; }
        public IndexModel(ICouponService couponService)
        {
            _couponService = couponService;

            var response = _couponService.GetAsync().GetAwaiter().GetResult();
            if (response.IsSuccess)
            {
                JArray jsonResponse = JArray.Parse(JsonConvert.SerializeObject(response.Data));

                Results = new List<CouponViewModel>();

                foreach (var item in jsonResponse)
                {
                    JObject coupon = (JObject)item;
                    Results.Add(JsonConvert.DeserializeObject<CouponViewModel>(coupon.ToString()));
                }
            }

        }

        [BindProperty(SupportsGet = true)]
        public string Query { get; set; }


        public IActionResult OnGet()
        {
            Results = string.IsNullOrEmpty(Query)
                ? Results
                : Results.Where(g => g.ToString().Contains(Query, StringComparison.OrdinalIgnoreCase)).ToList();

            ResultsCount = Results.Count();

            if (!Request.IsHtmx())
                return Page();

            Response.Htmx(h =>
            {
                //push current url into the history
                h.PushUrl(Request.GetEncodedUrl());
            });

            return Partial("Results", this);
        }

        public IActionResult OnGetCreateModal()
        {
            if (!Request.IsHtmx())
                return Page();

            Response.Htmx(h =>
            {
                h.PushUrl(Request.GetEncodedUrl());
            });

            return Partial("CreateModal", this);
        }

        public async Task<IActionResult> OnGetUpdateModal(int id)
        {
            TempData.Clear();
            ResponseInterface responseDto = await _couponService.GetAsync(id);
            if (!responseDto.IsSuccess || responseDto.Data == null)
            {
                ToastTitle = responseDto.Message;
                responseDto.Metadata.ToList().ForEach(x => TempData.Add(x, x));
                return RedirectToPage("Index", this);
            }
            RequestDto = JsonConvert.DeserializeObject<CouponDto>(((JObject)responseDto.Data).ToString());
            TempData["customErrors"] = null;
            TempData["customErrors"] = string.Join<string>("\n", responseDto.Metadata);

            if (!Request.IsHtmx())
                return Page();

            Response.Htmx(h =>
            {
                h.PushUrl(Request.GetEncodedUrl());
            });

            return Partial("EditModal", this);
        }

        public async Task<IActionResult> OnGetDeleteModal(int id)
        {
            TempData.Clear();
            var responseDto = await _couponService.GetAsync(id);
            if (!responseDto.IsSuccess || responseDto.Data == null)
            {
                ToastTitle = responseDto.Message;
                responseDto.Metadata.ToList().ForEach(x => TempData.Add(x, x));
                return RedirectToPage("Index", this);
            }
            RequestDto = JsonConvert.DeserializeObject<CouponDto>(((JObject)responseDto.Data).ToString());

            if (!Request.IsHtmx())
                return Page();

            Response.Htmx(h =>
            {
                h.PushUrl(Request.GetEncodedUrl());
            });

            return Partial("DeleteModal", this);
        }

        public async Task<IActionResult> OnPostSave(int id)
        {
            TempData.Clear();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            RequestDto.CouponId = id;
            if (RequestDto.CouponId != 0)
            {
                ResponseInterface responseDto = await _couponService.UpdateAsync(RequestDto);
                ToastTitle = responseDto.Message;
                responseDto.Metadata.ToList().ForEach(x => TempData.Add(x, x));
            }
            else
            {
                ResponseInterface responseDto = await _couponService.CreateAsync(RequestDto);
                ToastTitle = responseDto.Message;
                responseDto.Metadata.ToList().ForEach(x => TempData.Add(x, x));
            }
            return RedirectToPage("Index", this);
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            TempData.Clear();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (id != 0)
            {
                ResponseInterface responseDto = await _couponService.RemoveAsync(id);
                ToastTitle = responseDto.Message;
                responseDto.Metadata.ToList().ForEach(x => TempData.Add(x, x));
            }
            return RedirectToPage("Index", this);
        }
    }
}
