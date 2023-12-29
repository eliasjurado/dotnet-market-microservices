using Htmx;
using Market.Domain.Models.Dto.Services.Product;
using Market.Domain.Models.Dto.Web;
using Market.Web.Service.IService;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

namespace Market.Web.Pages.Product
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;
        [BindProperty(SupportsGet = true)]
        public ProductDto RequestDto { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public ProductViewModel model { get; set; }
        public ICollection<ProductViewModel> Results { get; set; }
        public int ResultsCount { get; set; } = 0;
        [BindProperty(SupportsGet = true)]
        public string Query { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ToastTitle { get; set; } = string.Empty;
        [BindProperty(SupportsGet = true)]
        public string ToastBody { get; set; }
        public IndexModel(IProductService productService)
        {
            _productService = productService;
            Categories = new List<SelectListItem>();
            Results = new List<ProductViewModel>();
            OnGetCategories();
            OnGetProducts();
        }

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
            var responseDto = await _productService.GetAsync(id);
            if (!responseDto.IsSuccess || responseDto.Data == null)
            {
                ToastTitle = responseDto.Message;
                responseDto.Metadata.ToList().ForEach(x => TempData.Add(x, x));
                return RedirectToPage("Index", this);
            }
            RequestDto = JsonConvert.DeserializeObject<ProductDto>(((JObject)responseDto.Data).ToString());

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
            var responseDto = await _productService.GetAsync(id);
            if (!responseDto.IsSuccess || responseDto.Data == null)
            {
                ToastTitle = responseDto.Message;
                responseDto.Metadata.ToList().ForEach(x => TempData.Add(x, x));
                return RedirectToPage("Index", this);
            }
            RequestDto = JsonConvert.DeserializeObject<ProductDto>(((JObject)responseDto.Data).ToString());

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
            RequestDto.Id = id;
            if (RequestDto.Id != 0)
            {
                ResponseInterface responseDto = await _productService.UpdateAsync(RequestDto);
                ToastTitle = responseDto.Message;
                responseDto.Metadata.ToList().ForEach(x => TempData.Add(x, x));
            }
            else
            {
                ResponseInterface responseDto = await _productService.CreateAsync(RequestDto);
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
                ResponseInterface responseDto = await _productService.RemoveAsync(id);
                ToastTitle = responseDto.Message;
                responseDto.Metadata.ToList().ForEach(x => TempData.Add(x, x));
            }
            return RedirectToPage("Index", this);
        }

        public void OnGetCategories()
        {
            var response = _productService.GetCategoriesAsync().GetAwaiter().GetResult();
            if (response.IsSuccess)
            {
                JArray jsonResponse = JArray.Parse(JsonConvert.SerializeObject(response.Data));
                Categories = new List<SelectListItem>();

                foreach (var item in jsonResponse)
                {
                    JObject role = (JObject)item;
                    Categories.Add(JsonConvert.DeserializeObject<SelectListItem>(role.ToString()));
                }
            }
        }

        public void OnGetProducts()
        {
            var response = _productService.GetAsync().GetAwaiter().GetResult();
            if (response.IsSuccess)
            {
                JArray jsonResponse = JArray.Parse(JsonConvert.SerializeObject(response.Data));
                foreach (var item in jsonResponse)
                {
                    JObject coupon = (JObject)item;
                    Results.Add(JsonConvert.DeserializeObject<ProductViewModel>(coupon.ToString()));
                }
            }
        }

    }
}
