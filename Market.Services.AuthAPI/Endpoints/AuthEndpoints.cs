using AutoMapper;
using FluentValidation;
using Market.Infrastructure;
using Market.Services.AuthAPI.Models;
using Market.Services.AuthAPI.Models.Dto;
using Market.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Mvc;
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

            app.MapPost("/api/roles", AssignRole)
                .WithName("AssignRole")
                .Accepts<SignUpRequestDto>("application/json")
                .Produces<ResponseDto<object>>(200)
                .Produces(400);
        }
        private async static Task<IResult> SignIn(IAuthService _service, IMapper _mapper, ILogger<Program> _logger, [FromBody] SignInRequestDto request)
        {
            ResponseDto<SignInResponseDto> response = new();
            try
            {
                _logger.Log(LogLevel.Information, "Getting user");


                var signInResponse = await _service.SignInAsync(request);

                if (signInResponse.User == null)
                {
                    response.Message = "UserName or Password is incorrect";
                    response.Errors.Add("UserName or Password is incorrect");
                    return Results.BadRequest(response);
                }

                response.IsSuccess = true;
                response.Data = signInResponse;
                response.StatusCode = HttpStatusCode.OK;
                response.Status = nameof(HttpStatusCode.OK);

                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = "Sign In process not completed";
                response.Errors = Format.GetInnerExceptionMessage(ex);
            }
            return Results.BadRequest(response);
        }

        private async static Task<IResult> SignUp(IAuthService _service, IMapper _mapper, ILogger<Program> _logger, [FromBody] SignUpRequestDto request)
        {
            ResponseDto<object> response = new();

            var messages = await _service.SignUpAsync(request);

            if (messages.Any())
            {
                response.Message = "User not created";
                response.Errors = messages;
                return Results.BadRequest(response);
            }

            _logger.Log(LogLevel.Information, "Creating user");
            response.IsSuccess = true;
            response.Message = "User created";
            response.Data = new object();
            response.StatusCode = HttpStatusCode.Created;
            response.Status = nameof(HttpStatusCode.Created);

            IResult result = Results.CreatedAtRoute("SignUp", new { status = HttpStatusCode.Created }, response);
            return result;
        }

        private async static Task<IResult> AssignRole(IAuthService _service, IMapper _mapper, ILogger<Program> _logger, [FromBody] RoleRequestDto request)
        {
            ResponseDto<object> response = new();
            try
            {
                response.Message = "Role for User not created";
                var isAssigned = await _service.AssignRoleAsync(request);

                if (!isAssigned)
                {
                    return Results.BadRequest(response);
                }

                _logger.Log(LogLevel.Information, "Creating user");
                response.IsSuccess = true;
                response.Message = "Role for User created";
                response.Data = new object();
                response.StatusCode = HttpStatusCode.Created;
                response.Status = nameof(HttpStatusCode.Created);

                IResult result = Results.CreatedAtRoute("AssignRole", new { status = HttpStatusCode.Created }, response);
                return result;
            }
            catch (Exception ex)
            {
                response.Errors = Format.GetInnerExceptionMessage(ex);
            }
            return Results.BadRequest(response); ;
        }
    }
}