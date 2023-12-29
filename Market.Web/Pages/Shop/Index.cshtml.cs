using AutoMapper;
using Market.Domain.Models.Dto.Services.Product;
using Market.Domain.Models.Dto.Web;
using Market.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Market.Web.Pages.Shop
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductViewModel Product { get; set; }
        public ICollection<ProductViewModel> Results { get; set; }
        public int ResultsCount { get; set; } = 0;
        [BindProperty(SupportsGet = true)]
        public string ToastTitle { get; set; } = string.Empty;
        [BindProperty(SupportsGet = true)]
        public ProductDto RequestDto { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; } = 0;
        public IndexModel(IProductService productService, IMapper mapper)
        {
            _mapper = mapper;
            _productService = productService;
            Results = new List<ProductViewModel>();
            OnGetProducts();
        }

        public void OnGet()
        {
            GetProductDetails();
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

        private void GetProductDetails()
        {
            RouteData.Values.TryGetValue("page", out object route);
            if (route.Equals("/Shop/ProductDetails"))
            {
                int.TryParse(RouteData.Values["id"].ToString(), out int outId);
                if (outId != 0)
                {
                    OnGetProduct(outId);
                }
            }
        }

        public void OnGetProduct(int id)
        {
            var response = _productService.GetAsync(id).GetAwaiter().GetResult();
            if (response.IsSuccess)
            {
                Product = JsonConvert.DeserializeObject<ProductViewModel>(response.Data.ToString());
            }
        }
        //public async Task<IActionResult> OnPostProductDetails(int id)
        //{
        //    TempData.Clear();
        //    var responseDto = await _productService.GetAsync(id);
        //    if (!responseDto.IsSuccess || responseDto.Data == null)
        //    {
        //        ToastTitle = responseDto.Message;
        //        responseDto.Metadata.ToList().ForEach(x => TempData.Add(x, x));
        //        return RedirectToPage("Index", this);
        //    }
        //    RequestDto = JsonConvert.DeserializeObject<ProductDto>(((JObject)responseDto.Data).ToString());
        //    var viewModel = _mapper.Map<ProductViewModel>(RequestDto);

        //    if (!Request.IsHtmx())
        //        return Page();

        //    Response.Htmx(h =>
        //    {
        //        h.PushUrl(Request.GetEncodedUrl());
        //    });

        //    return RedirectToPage("ProductDetails", viewModel);
        //}
    }
}
