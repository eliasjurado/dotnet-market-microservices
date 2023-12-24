using Market.Services.AuthAPI.Models.Dto;
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
