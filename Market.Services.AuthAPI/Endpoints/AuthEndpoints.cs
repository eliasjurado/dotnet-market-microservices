using AutoMapper;
using FluentValidation;
using Market.Domain.Models.Dto.Services.Auth;
using Market.Infrastructure;
using Market.Services.AuthAPI.Models;
using Market.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
namespace Market.Services.AuthAPI.Endpoints
{
    public static class AuthEndpoints
    {
        public static void ConfigureAuthEndpoints(this WebApplication app)
        {
            app.MapPost("/api/users/signin", SignIn)
                .WithName("SignIn")
                .Accepts<SignInRequestDto>("application/json")
                .Produces<ResponseDto<SignInResponseDto>>(200)
                .Produces(400);

            app.MapPost("/api/users/signup", SignUp)
                .WithName("SignUp")
                .Accepts<SignUpRequestDto>("application/json")
                .Produces<ResponseDto<object>>(201)
                .Produces(400);

            app.MapGet("/api/roles", GetRoles)
                .WithName("GetRoles")
                .Produces<ResponseDto<List<SelectListItem>>>(200)
                .Produces(400);

            app.MapPost("/api/roles", AssignRole)
                .WithName("AssignRole")
                .Accepts<SignUpRequestDto>("application/json")
                .Produces<ResponseDto<object>>(200)
                .Produces(400);
        }
        private async static Task<IResult> SignIn(IAuthService _service, IMapper _mapper, ILogger<Program> _logger, [FromBody] SignInRequestDto request)
        {
            ResponseDto<SignInResponseDto> response = new();
            response.Message = "Sign In failed";
            try
            {
                _logger.Log(LogLevel.Information, "SignIn user");


                var signInResponse = await _service.SignInAsync(request);

                if (signInResponse.User == null)
                {
                    response.Metadata.Add("UserName or Password is incorrect");
                    return Results.BadRequest(response);
                }

                response.Message = "Sign In successful";
                response.IsSuccess = true;
                response.Data = signInResponse;
                response.StatusCode = HttpStatusCode.OK;
                response.Status = nameof(HttpStatusCode.OK);

                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                response.Metadata = Format.GetInnerExceptionMessage(ex);
            }
            return Results.BadRequest(response);
        }

        private async static Task<IResult> SignUp(IAuthService _service, IMapper _mapper, ILogger<Program> _logger, [FromBody] SignUpRequestDto request)
        {
            ResponseDto<object> response = new();
            response.Message = "Sign Up failed";
            try
            {
                _logger.Log(LogLevel.Information, "Creating user");
                var messages = await _service.SignUpAsync(request);

                if (messages.Any())
                {
                    response.Metadata = messages;
                    return Results.BadRequest(response);
                }

                response.IsSuccess = true;
                response.Message = "Sign Up successful";
                response.Data = new object();
                response.StatusCode = HttpStatusCode.Created;
                response.Status = nameof(HttpStatusCode.Created);

                return Results.CreatedAtRoute("SignUp", new { statusCode = HttpStatusCode.Created }, response);
            }
            catch (Exception ex)
            {
                response.Metadata = Format.GetInnerExceptionMessage(ex);
            }
            return Results.BadRequest(response);
        }

        private async static Task<IResult> AssignRole(IAuthService _service, IMapper _mapper, ILogger<Program> _logger, [FromBody] RoleRequestDto request)
        {
            ResponseDto<object> response = new();
            try
            {
                response.Message = $"Role \"{request.Role}\" not assigned to User";
                var isAssigned = await _service.AssignRoleAsync(request);

                if (!isAssigned)
                {
                    return Results.BadRequest(response);
                }

                _logger.Log(LogLevel.Information, "Assigning role to user");
                response.IsSuccess = true;
                response.Message = $"Role \"{request.Role}\" assigned to User";
                //response.Data = new object();
                response.StatusCode = HttpStatusCode.Created;
                response.Status = nameof(HttpStatusCode.Created);

                IResult result = Results.CreatedAtRoute("AssignRole", new { statusCode = HttpStatusCode.Created }, response);
                return result;
            }
            catch (Exception ex)
            {
                response.Metadata = Format.GetInnerExceptionMessage(ex);
            }
            return Results.BadRequest(response);
        }

        private async static Task<IResult> GetRoles(IAuthService _service, IMapper _mapper, ILogger<Program> _logger)
        {
            ResponseDto<List<SelectListItem>> response = new();
            try
            {
                _logger.Log(LogLevel.Information, "Getting roles");
                response.Message = "Roles not retrieved";
                var roles = await _service.GetRolesAsync();

                if (!roles.Any())
                {
                    return Results.BadRequest(response);
                }

                response.IsSuccess = true;
                response.Message = "Roles retrieved";
                response.Data = roles.ToList();
                response.StatusCode = HttpStatusCode.OK;
                response.Status = nameof(HttpStatusCode.OK);

                IResult result = Results.Ok(response);
                return result;
            }
            catch (Exception ex)
            {
                response.Metadata = Format.GetInnerExceptionMessage(ex);
            }
            return Results.BadRequest(response);
        }
    }
}