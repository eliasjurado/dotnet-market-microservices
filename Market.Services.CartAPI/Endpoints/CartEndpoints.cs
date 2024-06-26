﻿using AutoMapper;
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

            app.MapGet("/api/Cart/{id}", GetCart)
                .WithName("GetCart")
                .Produces<ResponseDto<CartDto>>(200)
                .Produces(400);
            //.Produces(401)
            //.Produces(403)
            //.RequireAuthorization()

            app.MapPost("/api/Cart", SaveCart)
                .WithName("SaveCart")
                .Accepts<CartDto>("application/json")
                .Produces<ResponseDto<CartDto>>(201)
                .Produces<ResponseDto<CartDto>>(200)
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

            app.MapDelete("/api/Cart/", DeleteCart)
                .WithName("DeleteCart")
                .Produces<ResponseDto<CartDetail>>(200)
                .Produces(400);
            //.Produces(401)
            //.Produces(403)
            //.RequireAuthorization("AdminOnly");
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
        private async static Task<IResult> GetCart(HttpContext context, ICartHeaderRepository _headerRepository, ICartDetailRepository _detailRepository, IMapper _mapper, ILogger<Program> _logger, string id)
        {
            var userName = string.Empty;
            Guid userId;
            CartHeader header = new();
            ICollection<CartDetail> details = new List<CartDetail>();
            ResponseDto<CartDto> response = new();
            response.Message = "Get Cart Failed";
            try
            {
                int output = 0;
                var user = context.User.Claims.Where(c => c.Type.Equals(ClaimTypes.NameIdentifier)).Select(c => c.Value).SingleOrDefault();

                if (string.IsNullOrWhiteSpace(user))
                {
                    userId = Guid.NewGuid();
                    userName = Base.AnonymousUser;

                    if (!int.TryParse(id, out output))
                    {
                        response.Metadata.Add("Invalid Id was received");
                        return Results.BadRequest(response);
                    }

                    header = await _headerRepository.GetAsync(output);
                    details = await _detailRepository.GetAsync(output);
                }
                else
                {
                    userId = new Guid(user);
                    userName = context.User.Claims.Where(c => c.Type.Equals(ClaimTypes.Email)).Select(c => c.Value).SingleOrDefault();

                    header = await _headerRepository.GetAsync(userId);
                    details = await _detailRepository.GetAsync(userId);
                }

                response.Message = "Get Cart Succeeded";
                response.Data.CartDetails = _mapper.Map<List<CartDetailDto>>(details);
                response.Data.CartHeader = _mapper.Map<CartHeaderDto>(header);
                foreach (var item in response.Data.CartDetails)
                {
                    response.Data.CartHeader.CartTotal += (item.Quantity * item.ProductPrice);
                }
                response.Data.CartHeader.CartTotalDisccount = response.Data.CartHeader.CartTotal * (response.Data.CartHeader.CouponDisccountAmount * 0.01);
                response.Data.CartHeader.CartTotal = response.Data.CartHeader.CartTotal - response.Data.CartHeader.CartTotalDisccount;
                response.IsSuccess = true;
                response.Metadata.Add(userName.Equals(Base.AnonymousUser) ? $"Cart retrieved by CartID \"{output}\" successfully" : $"Cart retrieved for user \"{userName}\" successfully");
                response.StatusCode = HttpStatusCode.OK;
                response.Status = nameof(HttpStatusCode.OK);
                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                response.Metadata = Format.GetInnerExceptionMessage(ex);
                return Results.BadRequest(response);
            }
        }
        private async static Task<IResult> SaveCart(HttpContext context, ICartHeaderRepository _headerRepository, ICartDetailRepository _detailRepository, IMapper _mapper, ILogger<Program> _logger, IValidator<CartDto> _validator, [FromBody] CartDto request)
        {
            var userName = string.Empty;
            CartHeader header = new();
            List<CartDetail> details = new();
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
                    userName = Base.AnonymousUser;
                }
                else
                {
                    userId = new Guid(user);
                    userName = context.User.Claims.Where(c => c.Type.Equals(ClaimTypes.Email)).Select(c => c.Value).SingleOrDefault();
                }
                header = _mapper.Map<CartHeader>(request.CartHeader);
                header.CreatedBy = userId;
                header.UpdatedBy = userId;
                details = _mapper.Map<ICollection<CartDetail>>(request.CartDetails).ToList();
                details.ForEach(x => { x.CreatedBy = userId; x.UpdatedBy = userId; });

                var existingCartHeader = await _headerRepository.GetAsync(request.CartHeader.CreatedBy);
                if (existingCartHeader == null || request.CartHeader.CartHeaderId == 0)
                {
                    await _headerRepository.CreateAsync(header);
                    await _headerRepository.SaveAsync();

                    foreach (var row in details)
                    {
                        row.CartHeaderId = header.CartHeaderId;
                        await _detailRepository.CreateAsync(row);
                    }
                    await _detailRepository.SaveAsync();

                    response.IsSuccess = true;
                    response.Data = new CartDto
                    {
                        CartHeader = _mapper.Map<CartHeaderDto>(header),
                        CartDetails = _mapper.Map<List<CartDetailDto>>(details)
                    };
                    response.Message = "Cart Creation Succeeded";
                    response.Metadata.Add($"Cart was created by user \"{userName}\" successfully");
                    response.StatusCode = HttpStatusCode.Created;
                    response.Status = Format.GetName(nameof(HttpStatusCode.Created));
                    return Results.CreatedAtRoute("GetCart", new { id = header.CartHeaderId }, response);
                }
                else
                {
                    await _headerRepository.UpdateAsync(header);
                    await _headerRepository.SaveAsync();

                    await _detailRepository.RemoveAsync(userId, header.CartHeaderId);
                    await _detailRepository.SaveAsync();

                    foreach (var row in details)
                    {
                        await _detailRepository.CreateAsync(row);
                    }
                    await _detailRepository.SaveAsync();

                    response.IsSuccess = true;
                    response.Data = new CartDto
                    {
                        CartHeader = _mapper.Map<CartHeaderDto>(header),
                        CartDetails = _mapper.Map<List<CartDetailDto>>(details)
                    };
                    response.Message = "Update Cart Succeeded";
                    response.Metadata.Add($"Cart was updated by user \"{userName}\" successfully");
                    response.StatusCode = HttpStatusCode.OK;
                    response.Status = nameof(HttpStatusCode.OK);
                    return Results.Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Metadata = Format.GetInnerExceptionMessage(ex);
                return Results.BadRequest(response);
            }
        }

        private async static Task<IResult> DeleteCart(HttpContext context, ICartDetailRepository _detailRepository, IMapper _mapper, ILogger<Program> _logger, IValidator<CartDto> _validator, [FromBody] CartDetailDto request)
        {
            var userName = string.Empty;
            Guid userId;
            ResponseDto<CartDetailDto> response = new();
            response.Message = "Delete Cart Detail Failed";
            try
            {
                var user = context.User.Claims.Where(c => c.Type.Equals(ClaimTypes.NameIdentifier)).Select(c => c.Value).SingleOrDefault();

                if (string.IsNullOrWhiteSpace(user))
                {
                    response.Metadata.Add("To complete your purchase signin in Market or signup if you are not registered yet");
                    userId = Guid.NewGuid();
                    userName = Base.AnonymousUser;
                }
                else
                {
                    userId = new Guid(user);
                    userName = context.User.Claims.Where(c => c.Type.Equals(ClaimTypes.Email)).Select(c => c.Value).SingleOrDefault();
                }

                var existingDetail = await _detailRepository.GetAsync(request.CartHeaderId, request.CartDetailId);
                if (existingDetail == null)
                {
                    response.Metadata.Add("Cart Detail was already deleted");
                    return Results.BadRequest(response);
                }

                await _detailRepository.RemoveAsync(request.CartHeaderId, request.CartDetailId);
                await _detailRepository.SaveAsync();

                response.Message = "Delete Cart Detail Succeeded";
                response.Data = _mapper.Map<CartDetailDto>(existingDetail);
                response.Metadata.Add($"Cart was deleted by user \"{userName}\" successfully");
                response.StatusCode = HttpStatusCode.OK;
                response.Status = nameof(HttpStatusCode.OK);
                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                response.Metadata = Format.GetInnerExceptionMessage(ex);
                return Results.BadRequest(response);
            }
        }
        /* 
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
