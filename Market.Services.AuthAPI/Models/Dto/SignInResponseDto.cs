namespace Market.Services.AuthAPI.Models.Dto
{
    public class SignInResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
