namespace Market.Domain.Models.Dto.Services.Auth
{
    public class SignUpRequestDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string? Role { get; set; }
    }
}
