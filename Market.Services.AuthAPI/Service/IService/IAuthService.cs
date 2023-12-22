using Market.Services.AuthAPI.Models.Dto;

namespace Market.Services.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<ICollection<string>> SignUpAsync(SignUpRequestDto request);
        Task<SignInResponseDto> SignInAsync(SignInRequestDto request);
        Task<bool> AssignRoleAsync(RoleRequestDto request);
    }
}
