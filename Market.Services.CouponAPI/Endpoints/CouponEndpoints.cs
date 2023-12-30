using AutoMapper;
using FluentValidation;
using Market.Domain.Models;
using Market.Domain.Models.Dto.Services;
using Market.Domain.Models.Dto.Services.Coupon;
using Market.Infrastructure;
using Market.Services.CouponAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Market.Services.CouponAPI.Endpoints
{
    public static class CouponEndpoints
    {
        public static void ConfigureCouponEndpoints(this WebApplication app)
        {
            app.MapGet("/api/coupon", GetAllCoupon)
                .WithName("GetCoupons")
                .Produces<ResponseDto<List<CouponDto>>>(200)
                .Produces(401)
                .Produces(403)
                .RequireAuthorization();

            app.MapGet("/api/coupon/{id}", GetCoupon)
                .WithName("GetCoupon")
                .AddEndpointFilter(async (context, next) =>
                {
                    ResponseDto<object> response = new();
                    var id = context.GetArgument<string>(4);
                    int output;
                    if (!int.TryParse(id, out output))
                    {
                        response.Message = "Get Coupon Failed";
                        response.Metadata.Add("Invalid Id was received");
                        return Results.BadRequest(response);
                    }
                    if (output == 0)
                    {
                        response.Message = "Get Coupon Failed";
                        response.Metadata.Add("Id received cannot be zero");
                        return Results.BadRequest(response);
                    }
                    return await next(context);
                })
                .Produces<ResponseDto<CouponDto>>(200)
                .Produces(400)
                .Produces(401)
                .Produces(403)
                .RequireAuthorization();

            app.MapPost("/api/coupon", CreateCoupon)
                .WithName("CreateCoupon")
                .Accepts<CouponRequestDto>("application/json")
                .Produces<ResponseDto<CouponDto>>(201)
                .Produces(400)
                .Produces(401)
                .Produces(403)
                .RequireAuthorization("AdminOnly");

            app.MapPut("/api/coupon/{id}", UpdateCoupon)
                .WithName("UpdateCoupon")
                .Accepts<CouponRequestDto>("application/json")
                .Produces<ResponseDto<CouponDto>>(200)
                .Produces(400)
                .Produces(401)
                .Produces(403)
                .RequireAuthorization("AdminOnly");

            app.MapDelete("/api/coupon/{id}", DeleteCoupon)
                .WithName("DeleteCoupon")
                .Produces<ResponseDto<CouponDto>>(200)
                .Produces(400)
                .Produces(401)
                .Produces(403)
                .RequireAuthorization("AdminOnly");
        }

        private async static Task<IResult> GetAllCoupon(HttpContext context, IConfiguration _configuration, ICouponRepository _repository, IMapper _mapper, ILogger<Program> _logger)
        {
            ResponseDto<List<CouponDto>> response = new();
            response.Message = "Get All Coupons Failed";

            var userName = context.User.Claims.Where(c => c.Type.Equals(ClaimTypes.Name) || c.Type.Equals(ClaimTypes.Email)).Select(c => c.Value).SingleOrDefault();

            if (string.IsNullOrWhiteSpace(userName))
            {
                response.Metadata.Add("Invalid User Name was received");
                return Results.BadRequest(response);
            }

            _logger.Log(LogLevel.Information, "Getting all Coupons");
            response.IsSuccess = true;
            response.Message = "Get All Coupons Succedeed";
            response.Metadata.Add("Coupons were retrieved successfully");
            response.Data = (await _repository.GetAsync()).Select(_mapper.Map<CouponDto>).ToList();
            response.StatusCode = HttpStatusCode.OK;
            response.Status = nameof(HttpStatusCode.OK);

            return Results.Ok(response);
        }

        //[Authorize(Roles = "Admin,Manager")]
        private async static Task<IResult> GetCoupon(HttpContext context, ICouponRepository _repository, IMapper _mapper, ILogger<Program> _logger, string id)
        {
            ResponseDto<CouponDto> response = new();
            response.Message = "Get Coupon Failed";

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

            var couponDto = _mapper.Map<CouponDto>(await _repository.GetAsync(output));
            if (couponDto == null)
            {
                response.Metadata.Add($"Coupon with Id {id} not found");
                return Results.BadRequest(response);
            }

            response.IsSuccess = true;
            response.Message = "Get Coupon Succeeded";
            response.Metadata.Add($"Coupon {couponDto.Name} was retrieved successfully");
            response.Data = couponDto;
            response.StatusCode = HttpStatusCode.OK;
            response.Status = nameof(HttpStatusCode.OK);

            return Results.Ok(response);
        }

        private async static Task<IResult> CreateCoupon(HttpContext context, ICouponRepository _repository, IMapper _mapper, ILogger<Program> _logger, IValidator<CouponRequestDto> _validator, [FromBody] CouponRequestDto couponRequestDto)
        {
            var date = DateTime.Now;
            ResponseDto<CouponDto> response = new();
            response.Message = "Coupon Creation Failed";

            var user = context.User.Claims.Where(c => c.Type.Equals(ClaimTypes.Name) || c.Type.Equals(ClaimTypes.Email)).Select(c => c.Value).SingleOrDefault();

            if (string.IsNullOrWhiteSpace(user))
            {
                response.Metadata.Add("Invalid User Name was received");
                return Results.BadRequest(response);
            }
            var userName = new Guid(user);
            var validationResult = await _validator.ValidateAsync(couponRequestDto);

            if (!validationResult.IsValid)
            {
                validationResult.Errors.ForEach(x => response.Metadata.Add(x.ErrorMessage));
                return Results.BadRequest(response);
            }

            if (await _repository.GetAsync(couponRequestDto.Name) != null)
            {
                response.Metadata.Add("Coupon Name already exists");
                return Results.BadRequest(response);
            }

            var coupon = _mapper.Map<Coupon>(couponRequestDto);
            coupon.Name = couponRequestDto.Name;
            coupon.CouponDisccountAmount = couponRequestDto.CouponDisccountAmount;
            coupon.CouponMinAmmount = couponRequestDto.CouponMinAmmount;
            coupon.CouponStartDate = couponRequestDto.CouponStartDate;
            coupon.CouponEndDate = couponRequestDto.CouponEndDate;
            coupon.CreatedBy = userName;
            coupon.UpdatedBy = userName;

            await _repository.CreateAsync(coupon);
            await _repository.SaveAsync();

            response.IsSuccess = true;
            response.Message = "Coupon Creation Succeeded";
            response.Metadata.Add($"Coupon {coupon.Name} was created successfully");
            response.Data = _mapper.Map<CouponDto>(coupon);
            response.StatusCode = HttpStatusCode.Created;
            response.Status = Format.GetName(nameof(HttpStatusCode.Created));

            return Results.CreatedAtRoute("GetCoupon", new { id = coupon.CouponId }, response);
        }

        private async static Task<IResult> UpdateCoupon(HttpContext context, ICouponRepository _repository, IMapper _mapper, ILogger<Program> _logger, IValidator<CouponRequestDto> _validator, [FromBody] CouponRequestDto couponRequestDto, string id)
        {
            ResponseDto<CouponDto> response = new();
            response.Message = "Update Coupon Failed";

            var user = context.User.Claims.Where(c => c.Type.Equals(ClaimTypes.Name) || c.Type.Equals(ClaimTypes.Email)).Select(c => c.Value).SingleOrDefault();

            if (string.IsNullOrWhiteSpace(user))
            {
                response.Metadata.Add("Invalid User Name was received");
                return Results.BadRequest(response);
            }
            var userName = new Guid(user);
            var validationResult = await _validator.ValidateAsync(couponRequestDto);
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

            var existingCoupon = await _repository.GetAsync(couponRequestDto.Name);
            if (existingCoupon != null && existingCoupon.CouponId != output)
            {
                response.Metadata.Add("Coupon Name already exists");
                return Results.BadRequest(response);
            }

            var coupon = await _repository.GetAsync(output);

            if (coupon == null)
            {
                response.Metadata.Add($"Coupon with Id {id} not found");
                return Results.BadRequest(response);
            }

            coupon.CouponCode = couponRequestDto.CouponCode;
            coupon.Name = couponRequestDto.Name;
            coupon.CouponDisccountAmount = couponRequestDto.CouponDisccountAmount;
            coupon.CouponMinAmmount = couponRequestDto.CouponMinAmmount;
            coupon.CouponStartDate = couponRequestDto.CouponStartDate;
            coupon.CouponEndDate = couponRequestDto.CouponEndDate;
            coupon.UpdatedBy = userName;
            coupon.UpdatedAt = DateTime.Now;

            await _repository.UpdateAsync(coupon);
            await _repository.SaveAsync();

            response.IsSuccess = true;
            response.Message = "Update Product Succeeded";
            response.Metadata.Add($"Product {coupon.Name} was updated successfully");
            response.Data = _mapper.Map<CouponDto>(coupon);
            response.StatusCode = HttpStatusCode.OK;
            response.Status = nameof(HttpStatusCode.OK);

            return Results.Ok(response);
        }

        private async static Task<IResult> DeleteCoupon(HttpContext context, ICouponRepository _repository, IMapper _mapper, ILogger<Program> _logger, string id)
        {
            ResponseDto<CouponDto> response = new();
            response.Message = "Delete Coupon Failed";

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

            var coupon = await _repository.GetAsync(output);

            if (coupon == null)
            {
                response.Metadata.Add($"Coupon with Id {id} not found");
                return Results.BadRequest(response);
            }

            response.Data = _mapper.Map<CouponDto>(coupon);
            await _repository.RemoveAsync(output);
            await _repository.SaveAsync();
            response.IsSuccess = true;
            response.Message = "Delete Coupon Succeeded";
            response.Metadata.Add($"Coupon {coupon.Name} was deleted successfully");
            response.StatusCode = HttpStatusCode.OK;
            response.Status = nameof(HttpStatusCode.OK);

            return Results.Ok(response);
        }
    }
}
