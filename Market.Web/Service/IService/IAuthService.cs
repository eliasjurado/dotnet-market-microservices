
using Market.Web.Models.Dto;

namespace Market.Web.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto?> SignUpAsync(SignUpRequestDto requestDto);
        Task<ResponseDto?> SignInAsync(SignInRequestDto requestDto);
        Task<ResponseDto?> AssignRoleAsync(RoleRequestDto requestDto);
        Task<ResponseDto?> GetRolesAsync();
    }
}
