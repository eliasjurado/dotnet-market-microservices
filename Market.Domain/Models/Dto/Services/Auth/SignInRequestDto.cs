using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Models.Dto.Services.Auth
{
    public class SignInRequestDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
