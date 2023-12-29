using AutoMapper;
using Htmx;
using Market.Domain.Models.Dto.Services.Product;
using Market.Domain.Models.Dto.Web;
using Market.Web.Service.IService;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Market.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public ICollection<ProductViewModel> Results { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ToastTitle { get; set; } = string.Empty;
        [BindProperty(SupportsGet = true)]
        public ProductDto RequestDto { get; set; }
        public IndexModel(IProductService productService, IMapper mapper)
        {
            _mapper = mapper;
            _productService = productService;
            Results = new List<ProductViewModel>();
            OnGetProducts();
        }
        public void OnGet()
        {
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

        public async Task<IActionResult> OnGetProductDetails(int id)
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
            var viewModel = new ProductViewModel
            {
                Id = RequestDto.Id,
                Name = RequestDto.Name,
                CategoryName = RequestDto.CategoryName,
                Description = RequestDto.Description,
                ImageUrl = RequestDto.ImageUrl,
                Price = RequestDto.Price
            };

            if (!Request.IsHtmx())
                return Page();

            Response.Htmx(h =>
            {
                h.PushUrl(Request.GetEncodedUrl());
            });

            return RedirectToPage("./Shop/ProductDetails", viewModel);
        }
    }
}
