using Market.Infrastructure;
using Market.Web.Service.IService;

namespace Market.Web.Service
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public TokenProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public void ClearToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete(Base.TokenCookie);
        }

        public string GetToken()
        {
            var token = string.Empty;
            var hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(Base.TokenCookie, out token);
            return hasToken is true ? token : string.Empty;
        }

        public void SetToken(string token)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append(Base.TokenCookie, token);
        }
    }
}
