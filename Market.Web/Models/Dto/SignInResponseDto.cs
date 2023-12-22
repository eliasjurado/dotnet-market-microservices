namespace Market.Web.Models.Dto
{
    public class SignInResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
