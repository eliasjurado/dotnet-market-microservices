namespace Market.Services.CouponAPI.Endpoints
{
    public static class AuthEndpoints
    {
        //public static void ConfigureAuthEndpoints(this WebApplication app)
        //{
        //    app.MapPost("/api/signin", SignIn)
        //        .WithName("SignIn")
        //        .Accepts<SignInRequestDTO>("application/json")
        //        .Produces<APIResponse<SignInResponseDTO>>(200)
        //        .Produces(400);

        //    app.MapPost("/api/signup", SignUp)
        //        .WithName("SignUp")
        //        .Accepts<SignUpRequestDTO>("application/json")
        //        .Produces<APIResponse<UserDTO>>(200)
        //        .Produces(400);
        //}
        //private async static Task<IResult> SignIn(IAuthRepository _repository, IMapper _mapper, ILogger<Program> _logger, [FromBody] SignInRequestDTO request)
        //{
        //    APIResponse<SignInResponseDTO> response = new();
        //    var signInResponse = await _repository.SignIn(request);

        //    if (signInResponse == null)
        //    {
        //        response.Errors.Add("UserName or Password is incorrect");
        //        return Results.BadRequest(response);
        //    }

        //    _logger.Log(LogLevel.Information, "Getting user");
        //    response.IsSuccess = true;
        //    response.Result = signInResponse;
        //    response.StatusCode = HttpStatusCode.OK;
        //    response.Status = nameof(HttpStatusCode.OK);

        //    return Results.Ok(response);
        //}

        //private async static Task<IResult> SignUp(IAuthRepository _repository, IMapper _mapper, ILogger<Program> _logger, [FromBody] SignUpRequestDTO request)
        //{
        //    APIResponse<UserDTO> response = new();

        //    bool ifUserNameIsUnique = await _repository.IsUniqueUser(request.UserName);
        //    if (!ifUserNameIsUnique)
        //    {
        //        response.Errors.Add("UserName already registered");
        //        return Results.BadRequest(response);
        //    }

        //    var signUpResponse = await _repository.SignUp(request);

        //    if (signUpResponse == null || string.IsNullOrWhiteSpace(signUpResponse.UserName))
        //    {
        //        response.Errors.Add("User not created. Try again");
        //        return Results.BadRequest(response);
        //    }

        //    _logger.Log(LogLevel.Information, "Creating user");
        //    response.IsSuccess = true;
        //    response.Result = signUpResponse;
        //    response.StatusCode = HttpStatusCode.OK;
        //    response.Status = nameof(HttpStatusCode.OK);

        //    return Results.Ok(response);
        //}

    }
}
