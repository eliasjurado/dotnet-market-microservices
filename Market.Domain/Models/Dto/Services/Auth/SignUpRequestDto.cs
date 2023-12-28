using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Models.Dto.Services.Auth
{
    public sealed class SignUpRequestDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string? Role { get; set; }
    }
}
