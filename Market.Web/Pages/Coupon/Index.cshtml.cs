﻿using AutoMapper;
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
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        private readonly ICouponService _couponService;
        private readonly IMapper _mapper;
        [BindProperty(SupportsGet = true)]
        public CouponViewModel model { get; set; }
        public ICollection<CouponViewModel> Results { get; set; }
        [BindProperty(SupportsGet = true)]
        public CouponDto RequestDto { get; set; }
        public ResponseDto ResponseDto { get; set; }
        public IndexModel(ICouponService couponService, IMapper mapper)
        {
            _mapper = mapper;
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
            RequestDto = JsonConvert.DeserializeObject<CouponDto>(((JObject)(await _couponService.GetAsync(id)).Data).ToString());

            if (!Request.IsHtmx())
                return Page();

            Response.Htmx(h =>
            {
                h.PushUrl(Request.GetEncodedUrl());
            });

            return Partial("EditModal", this);
        }

        public async Task<IActionResult> OnPostSave(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            RequestDto.CouponId = id;
            if (RequestDto.CouponId != 0)
            {
                await _couponService.UpdateAsync(RequestDto);
            }
            else
            {
                await _couponService.CreateAsync(RequestDto);
            }
            return RedirectToPage("Index");
        }
    }
}
