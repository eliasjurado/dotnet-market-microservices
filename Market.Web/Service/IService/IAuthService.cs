
using Market.Domain.Models.Dto.Services.Auth;
using Market.Domain.Models.Dto.Web;


namespace Market.Web.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseInterface?> SignUpAsync(SignUpRequestDto requestDto);
        Task<ResponseInterface?> SignInAsync(SignInRequestDto requestDto);
        Task<ResponseInterface?> AssignRoleAsync(RoleRequestDto requestDto);
        Task<ResponseInterface?> GetRolesAsync();
    }
}
