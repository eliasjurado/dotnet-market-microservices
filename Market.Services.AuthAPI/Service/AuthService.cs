using AutoMapper;
using Market.Infrastructure;
using Market.Services.AuthAPI.Data;
using Market.Services.AuthAPI.Models;
using Market.Services.AuthAPI.Models.Dto;
using Market.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Market.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IMapper _mapper;

        public AuthService(ApplicationDbContext db, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<SignInResponseDto> SignInAsync(SignInRequestDto request)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == request.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (user == null || isValid == false)
            {
                return new SignInResponseDto() { User = null, Token = string.Empty };
            }

            //if user was found , Generate JWT Token
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            UserDto userDTO = new()
            {
                Email = user.Email,
                UserName = user.UserName,
                Id = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };

            var loginResponseDto = new SignInResponseDto()
            {
                User = userDTO,
                Token = token
            };

            return loginResponseDto;
        }

        public async Task<ICollection<string>> SignUpAsync(SignUpRequestDto request)
        {

            ApplicationUser user = new()
            {
                UserName = request.UserName,
                Email = request.Email,
                NormalizedEmail = request.Email.ToUpper(),
                Name = request.Name,
                PhoneNumber = request.PhoneNumber
            };
            List<string> messages = new();
            try
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    var dbUser = _mapper.Map<UserDto>(await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName == request.Email));
                }
                else
                {
                    messages.AddRange(result.Errors.Select(x => x.Description.ToString()));
                }
            }
            catch (Exception ex)
            {
                messages.AddRange(Format.GetInnerExceptionMessage(ex));
            }
            return messages;
        }

        public async Task<bool> AssignRoleAsync(RoleRequestDto request)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == request.Role.ToLower());

            if (user == null)
                return false;

            if (!_roleManager.RoleExistsAsync(request.Role).GetAwaiter().GetResult())
            {
                //create role if it does not exist
                _roleManager.CreateAsync(new IdentityRole(request.Role)).GetAwaiter().GetResult();
            }
            await _userManager.AddToRoleAsync(user, request.Role);
            return true;
        }
    }
}
