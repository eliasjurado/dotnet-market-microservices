using Market.Domain.Models.Dto.Services.Auth;
using Market.Domain.Models.Dto.Web;
using Market.Web.Service.IService;
using static Market.Infrastructure.Base;

namespace Market.Web.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseInterface> AssignRoleAsync(RoleRequestDto requestDto)
        {
            var request = new RequestInterface
            {
                ApiType = ApiType.POST,
                Url = AuthAPIBase + "/api/roles",
                Data = requestDto
            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseInterface> SignInAsync(SignInRequestDto requestDto)
        {
            var request = new RequestInterface
            {
                ApiType = ApiType.POST,
                Url = AuthAPIBase + "/api/users/signin",
                Data = requestDto
            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseInterface> SignUpAsync(SignUpRequestDto requestDto)
        {
            var request = new RequestInterface
            {
                ApiType = ApiType.POST,
                Url = AuthAPIBase + "/api/users/signup",
                Data = requestDto
            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseInterface> GetRolesAsync()
        {
            var request = new RequestInterface
            {
                ApiType = ApiType.GET,
                Url = AuthAPIBase + "/api/roles",
            };
            return await _baseService.SendAsync(request);
        }
    }
}
