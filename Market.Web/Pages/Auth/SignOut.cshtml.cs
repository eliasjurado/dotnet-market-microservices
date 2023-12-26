using Market.Web.Service.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Market.Web.Pages.Auth
{
    public class SignOutModel : PageModel
    {
        private readonly ITokenProvider _tokenProvider;
        public SignOutModel(ITokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        public async Task<IActionResult> OnGet()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToPage("Index");
        }
    }
}
