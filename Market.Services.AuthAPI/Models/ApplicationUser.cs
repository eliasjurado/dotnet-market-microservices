using Microsoft.AspNetCore.Identity;

namespace Market.Services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
