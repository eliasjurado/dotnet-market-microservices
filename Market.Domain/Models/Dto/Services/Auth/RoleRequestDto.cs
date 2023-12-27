using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Models.Dto.Services.Auth
{
    public class RoleRequestDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
