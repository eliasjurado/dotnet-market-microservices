namespace Market.Domain.Models.Dto.Services.Auth
{
    public sealed class SignInResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
