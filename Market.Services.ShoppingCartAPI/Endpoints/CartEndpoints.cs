using AutoMapper;
using FluentValidation;
using Market.Domain.Models;
using Market.Domain.Models.Dto.Services;
using Market.Domain.Models.Dto.Services.Cart;
using Market.Infrastructure;
using Market.Services.CartAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

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

            app.MapPut("/api/Cart/{id}", UpdateCart)
                .WithName("UpdateCart")
                .Accepts<CartDto>("application/json")
                .Produces<ResponseDto<CartDto>>(200)
                .Produces(400);
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
            var userName = string.Empty;
            CartHeader header = new();
            Guid userId;
            ResponseDto<CartDto> response = new();
            response.Message = "Cart Creation Failed";
            try
            {
                var user = context.User.Claims.Where(c => c.Type.Equals(ClaimTypes.NameIdentifier)).Select(c => c.Value).SingleOrDefault();

                if (string.IsNullOrWhiteSpace(user))
                {
                    response.Metadata.Add("To complete your purchase signin in Market or signup if you are not registered yet");
                    userId = Guid.NewGuid();
                    userName = "Visitor";
                }
                else
                {
                    userId = new Guid(user);
                    userName = context.User.Claims.Where(c => c.Type.Equals(ClaimTypes.Email)).Select(c => c.Value).SingleOrDefault();
                }

                var existingCartHeader = await _headerRepository.GetAsync(request.CartHeader.CreatedBy);
                if (existingCartHeader == null)
                {
                    //create header and details
                    header = _mapper.Map<CartHeader>(request.CartHeader);
                    header.CreatedBy = userId;
                    header.UpdatedBy = userId;
                    await _headerRepository.CreateAsync(header);
                    await _headerRepository.SaveAsync();

                    var details = _mapper.Map<ICollection<CartDetail>>(request.CartDetails);
                    foreach (var detail in details)
                    {
                        detail.CreatedBy = userId;
                        detail.UpdatedBy = userId;
                        await _detailRepository.CreateAsync(detail);
                    }
                    await _detailRepository.SaveAsync();

                    response.IsSuccess = true;
                    response.Message = "Cart Creation Succeeded";
                    response.Metadata.Add($"Cart was created by user \"{userName}\" successfully");
                    response.StatusCode = HttpStatusCode.Created;
                    response.Status = Format.GetName(nameof(HttpStatusCode.Created));
                }
            }
            catch (Exception ex)
            {
                response.Metadata = Format.GetInnerExceptionMessage(ex);
                return Results.BadRequest(response);
            }

            return Results.CreatedAtRoute("GetCart", new { id = header.CartHeaderId }, response);
        }

        private async static Task<IResult> UpdateCart(HttpContext context, ICartHeaderRepository _headerRepository, ICartDetailRepository _detailRepository, IMapper _mapper, ILogger<Program> _logger, IValidator<CartDto> _validator, [FromBody] CartDto request, string id)
        {
            var userName = string.Empty;
            CartHeader header = new();
            Guid userId;
            ResponseDto<CartDto> response = new();
            response.Message = "Update Cart Failed";
            try
            {
                var user = context.User.Claims.Where(c => c.Type.Equals(ClaimTypes.NameIdentifier)).Select(c => c.Value).SingleOrDefault();

                if (string.IsNullOrWhiteSpace(user))
                {
                    response.Metadata.Add("To complete your purchase signin in Market or signup if you are not registered yet");
                    userId = Guid.NewGuid();
                    userName = "Visitor";
                }
                else
                {
                    userId = new Guid(user);
                    userName = context.User.Claims.Where(c => c.Type.Equals(ClaimTypes.Email)).Select(c => c.Value).SingleOrDefault();
                }

                var existingCartHeader = await _headerRepository.GetAsync(request.CartHeader.CreatedBy);
                if (existingCartHeader == null)
                {
                    //create header and details
                    header = _mapper.Map<CartHeader>(request.CartHeader);
                    header.CreatedBy = userId;
                    header.UpdatedBy = userId;
                    await _headerRepository.CreateAsync(header);
                    await _headerRepository.SaveAsync();

                    var details = _mapper.Map<ICollection<CartDetail>>(request.CartDetails);
                    foreach (var detail in details)
                    {
                        detail.CreatedBy = userId;
                        detail.UpdatedBy = userId;
                        await _detailRepository.CreateAsync(detail);
                    }
                    await _detailRepository.SaveAsync();

                    response.IsSuccess = true;
                    response.Message = "Update Cart Succeeded";
                    response.Metadata.Add($"Cart was updated by user \"{userName}\" successfully");
                    response.StatusCode = HttpStatusCode.OK;
                    response.Status = nameof(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                response.Metadata = Format.GetInnerExceptionMessage(ex);
                return Results.BadRequest(response);
            }

            return Results.Ok(response);
        }
        /*
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
