
using Market.Web.Models.Dto;

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
