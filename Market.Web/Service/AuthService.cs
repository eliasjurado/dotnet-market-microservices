using Market.Web.Models.Dto;
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
        public async Task<ResponseDto> AssignRoleAsync(RoleRequestDto requestDto)
        {
            var request = new RequestDto
            {
                ApiType = ApiType.POST,
                Url = AuthAPIBase + "/api/roles",
                Data = requestDto
            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseDto> SignInAsync(SignInRequestDto requestDto)
        {
            var request = new RequestDto
            {
                ApiType = ApiType.POST,
                Url = AuthAPIBase + "/api/users/signin",
                Data = requestDto
            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseDto> SignUpAsync(SignUpRequestDto requestDto)
        {
            var request = new RequestDto
            {
                ApiType = ApiType.POST,
                Url = AuthAPIBase + "/api/users/signup",
                Data = requestDto
            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseDto> GetRolesAsync()
        {
            var request = new RequestDto
            {
                ApiType = ApiType.GET,
                Url = AuthAPIBase + "/api/roles",
            };
            return await _baseService.SendAsync(request);
        }
    }
}
