using Market.Domain.Models.Dto.Services.Auth;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Market.Services.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<ICollection<string>> SignUpAsync(SignUpRequestDto request);
        Task<SignInResponseDto> SignInAsync(SignInRequestDto request);
        Task<bool> AssignRoleAsync(RoleRequestDto request);
        Task<ICollection<SelectListItem>> GetRolesAsync();
    }
}
