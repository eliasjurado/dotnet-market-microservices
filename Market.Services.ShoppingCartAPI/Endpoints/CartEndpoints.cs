using AutoMapper;
using FluentValidation;
using Market.Domain.Models.Dto.Services;
using Market.Domain.Models.Dto.Services.Cart;
using Market.Infrastructure;
using Market.Services.CartAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Market.Services.CartAPI.Endpoints
{
    public static class CartEndpoints
    {
        public static void ConfigureCartEndpoints(this WebApplication app)
        {
            //app.MapGet("/api/Cart", GetAllCart)
            //    .WithName("GetCarts")
            //    .Produces<ResponseDto<List<CartDto>>>(200)
            //    .Produces(401)
            //    .Produces(403)
            //    .RequireAuthorization();

            //app.MapGet("/api/Cart/{id}", GetCart)
            //    .WithName("GetCart")
            //    .AddEndpointFilter(async (context, next) =>
            //    {
            //        ResponseDto<object> response = new();
            //        var id = context.GetArgument<string>(4);
            //        int output;
            //        if (!int.TryParse(id, out output))
            //        {
            //            response.Message = "Get Cart Failed";
            //            response.Metadata.Add("Invalid Id was received");
            //            return Results.BadRequest(response);
            //        }
            //        if (output == 0)
            //        {
            //            response.Message = "Get Cart Failed";
            //            response.Metadata.Add("Id received cannot be zero");
            //            return Results.BadRequest(response);
            //        }
            //        return await next(context);
            //    })
            //    .Produces<ResponseDto<CartDto>>(200)
            //    .Produces(400)
            //    .Produces(401)
            //    .Produces(403)
            //    .RequireAuthorization();

            app.MapPost("/api/Cart", CreateCart)
                .WithName("CreateCart")
                .Accepts<CartDto>("application/json")
                .Produces<ResponseDto<CartDto>>(201)
                .Produces(400);
            //.Produces(401)
            //.Produces(403)
            //.RequireAuthorization("AdminOnly")

            //app.MapPut("/api/Cart/{id}", UpdateCart)
            //    .WithName("UpdateCart")
            //    .Accepts<CartDto>("application/json")
            //    .Produces<ResponseDto<CartDto>>(200)
            //    .Produces(400)
            //    .Produces(401)
            //    .Produces(403)
            //    .RequireAuthorization("AdminOnly");

            //app.MapDelete("/api/Cart/{id}", DeleteCart)
            //    .WithName("DeleteCart")
            //    .Produces<ResponseDto<CartDto>>(200)
            //    .Produces(400)
            //    .Produces(401)
            //    .Produces(403)
            //    .RequireAuthorization("AdminOnly");
        }
        /*
                private async static Task<IResult> GetAllCart(HttpContext context, IConfiguration _configuration, ICartHeaderRepository _headerRepository, ICartDetailRepository _detailRepository, IMapper _mapper, ILogger<Program> _logger)
                {
                    ResponseDto<List<CartDto>> response = new();
                    response.Message = "Get All Carts Failed";

                    var userName = context.User.Claims.Where(c => c.Type.Equals(ClaimTypes.Name) || c.Type.Equals(ClaimTypes.Email)).Select(c => c.Value).SingleOrDefault();

                    if (string.IsNullOrWhiteSpace(userName))
                    {
                        response.Metadata.Add("Invalid User Name was received");
                        return Results.BadRequest(response);
                    }

                    _logger.Log(LogLevel.Information, "Getting all Carts");
                    response.IsSuccess = true;
                    response.Message = "Get All Carts Succedeed";
                    response.Metadata.Add("Carts were retrieved successfully");
                    response.Data = (await _headerRepository.GetAsync()).Select(_mapper.Map<CartDto>).ToList();
                    response.StatusCode = HttpStatusCode.OK;
                    response.Status = nameof(HttpStatusCode.OK);

                    return Results.Ok(response);
                }


                //[Authorize(Roles = "Admin,Manager")]
                private async static Task<IResult> GetCart(HttpContext context, ICartHeaderRepository _headerRepository, ICartDetailRepository _detailRepository, IMapper _mapper, ILogger<Program> _logger, string id)
                {
                    ResponseDto<CartDto> response = new();
                    response.Message = "Get Cart Failed";

                    var userName = context.User.Claims.Where(c => c.Type.Equals(ClaimTypes.Name) || c.Type.Equals(ClaimTypes.Email)).Select(c => c.Value).SingleOrDefault();

                    if (string.IsNullOrWhiteSpace(userName))
                    {
                        response.Metadata.Add("Invalid User Name was received");
                        return Results.BadRequest(response);
                    }

                    int output;
                    if (!int.TryParse(id, out output))
                    {
                        response.Metadata.Add("Invalid Id was received");
                        return Results.BadRequest(response);
                    }

                    //var CartDto = _mapper.Map<CartDto>(await _headerRepository.GetAsync(output));
                    //if (CartDto == null)
                    //{
                    //    response.Metadata.Add($"Cart with Id {id} not found");
                    //    return Results.BadRequest(response);
                    //}

                    response.IsSuccess = true;
                    response.Message = "Get Cart Succeeded";
                    //response.Metadata.Add($"Cart {CartDto.Name} was retrieved successfully");
                    //response.Data = CartDto;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Status = nameof(HttpStatusCode.OK);

                    return Results.Ok(response);
                }*/

        private async static Task<IResult> CreateCart(HttpContext context, ICartHeaderRepository _headerRepository, ICartDetailRepository _detailRepository, IMapper _mapper, ILogger<Program> _logger, IValidator<CartDto> _validator, [FromBody] CartDto request)
        {
            var date = DateTime.Now;
            ResponseDto<CartDto> response = new();
            response.Message = "Cart Creation Failed";
            try
            {
                var existingCartHeader = await _headerRepository.GetAsync(request.CartHeader.CreatedBy);
            }
            catch (Exception ex)
            {
                response.Metadata = Format.GetInnerExceptionMessage(ex);
                return Results.BadRequest(response);
            }

            //var userName = context.User.Claims.Where(c => c.Type.Equals(ClaimTypes.Name) || c.Type.Equals(ClaimTypes.Email)).Select(c => c.Value).SingleOrDefault();

            //if (string.IsNullOrWhiteSpace(userName))
            //{
            //    response.Metadata.Add("Invalid User Name was received");
            //    return Results.BadRequest(response);
            //}

            //var validationResult = await _validator.ValidateAsync(CartDto);

            //if (!validationResult.IsValid)
            //{
            //    validationResult.Errors.ForEach(x => response.Metadata.Add(x.ErrorMessage));
            //    return Results.BadRequest(response);
            //}

            //if (await _headerRepository.GetAsync(CartDto.Name) != null)
            //{
            //    response.Metadata.Add("Cart Name already exists");
            //    return Results.BadRequest(response);
            //}

            //var Cart = _mapper.Map<Cart>(CartDto);
            //Cart.Name = CartDto.Name;
            //Cart.CartDisccountAmount = CartDto.CartDisccountAmount;
            //Cart.CartMinAmmount = CartDto.CartMinAmmount;
            //Cart.CartStartDate = CartDto.CartStartDate;
            //Cart.CartEndDate = CartDto.CartEndDate;
            //Cart.CreatedBy = userName;
            //Cart.UpdatedBy = userName;

            //await _headerRepository.CreateAsync(Cart);
            //await _headerRepository.SaveAsync();

            response.IsSuccess = true;
            response.Message = "Cart Creation Succeeded";
            //response.Metadata.Add($"Cart {re.Name} was created successfully");
            //response.Data = _mapper.Map<CartDto>(Cart);
            response.StatusCode = HttpStatusCode.Created;
            response.Status = Format.GetName(nameof(HttpStatusCode.Created));

            //return Results.CreatedAtRoute("GetCart", new { id = Cart.CartId }, response);
            return Results.CreatedAtRoute("GetCart", new { id = 1 }, response);
        }
        /*
                private async static Task<IResult> UpdateCart(HttpContext context, ICartHeaderRepository _headerRepository, ICartDetailRepository _detailRepository, IMapper _mapper, ILogger<Program> _logger, IValidator<CartDto> _validator, [FromBody] CartDto CartDto, string id)
                {
                    ResponseDto<CartDto> response = new();
                    response.Message = "Update Cart Failed";

                    var userName = context.User.Claims.Where(c => c.Type.Equals(ClaimTypes.Name) || c.Type.Equals(ClaimTypes.Email)).Select(c => c.Value).SingleOrDefault();

                    if (string.IsNullOrWhiteSpace(userName))
                    {
                        response.Metadata.Add("Invalid User Name was received");
                        return Results.BadRequest(response);
                    }

                    var validationResult = await _validator.ValidateAsync(CartDto);
                    if (!validationResult.IsValid)
                    {
                        validationResult.Errors.ForEach(x => response.Metadata.Add(x.ErrorMessage));
                        return Results.BadRequest(response);
                    }

                    int output;
                    if (!int.TryParse(id, out output))
                    {
                        response.Metadata.Add("Invalid Id was received");
                        return Results.BadRequest(response);
                    }

                    //var existingCart = await _headerRepository.GetAsync(CartDto.Name);
                    //if (existingCart != null && existingCart.CartId != output)
                    //{
                    //    response.Metadata.Add("Cart Name already exists");
                    //    return Results.BadRequest(response);
                    //}

                    var Cart = await _headerRepository.GetAsync(output);

                    if (Cart == null)
                    {
                        response.Metadata.Add($"Cart with Id {id} not found");
                        return Results.BadRequest(response);
                    }

                    Cart.CartCode = CartDto.CartCode;
                    Cart.Name = CartDto.Name;
                    Cart.CartDisccountAmount = CartDto.CartDisccountAmount;
                    Cart.CartMinAmmount = CartDto.CartMinAmmount;
                    Cart.CartStartDate = CartDto.CartStartDate;
                    Cart.CartEndDate = CartDto.CartEndDate;
                    Cart.UpdatedBy = userName;
                    Cart.UpdatedAt = DateTime.Now;

                    await _headerRepository.UpdateAsync(Cart);
                    await _headerRepository.SaveAsync();

                    response.IsSuccess = true;
                    response.Message = "Update Product Succeeded";
                    response.Metadata.Add($"Product {Cart.Name} was updated successfully");
                    response.Data = _mapper.Map<CartDto>(Cart);
                    response.StatusCode = HttpStatusCode.OK;
                    response.Status = nameof(HttpStatusCode.OK);

                    return Results.Ok(response);
                }

                private async static Task<IResult> DeleteCart(HttpContext context, ICartHeaderRepository _headerRepository, ICartDetailRepository _detailRepository, IMapper _mapper, ILogger<Program> _logger, string id)
                {
                    ResponseDto<CartDto> response = new();
                    response.Message = "Delete Cart Failed";

                    var userName = context.User.Claims.Where(c => c.Type.Equals(ClaimTypes.Name) || c.Type.Equals(ClaimTypes.Email)).Select(c => c.Value).SingleOrDefault();

                    if (string.IsNullOrWhiteSpace(userName))
                    {
                        response.Metadata.Add("Invalid User Name was received");
                        return Results.BadRequest(response);
                    }

                    int output;
                    if (!int.TryParse(id, out output))
                    {
                        response.Metadata.Add("Invalid Id was received");
                        return Results.BadRequest(response);
                    }

                    var Cart = await _headerRepository.GetAsync(output);

                    if (Cart == null)
                    {
                        response.Metadata.Add($"Cart with Id {id} not found");
                        return Results.BadRequest(response);
                    }

                    response.Data = _mapper.Map<CartDto>(Cart);
                    //await _headerRepository.RemoveAsync(Cart);
                    await _headerRepository.SaveAsync();
                    response.IsSuccess = true;
                    response.Message = "Delete Cart Succeeded";
                    response.Metadata.Add($"Cart {Cart.Name} was deleted successfully");
                    response.StatusCode = HttpStatusCode.OK;
                    response.Status = nameof(HttpStatusCode.OK);

                    return Results.Ok(response);
                }*/
    }
}
