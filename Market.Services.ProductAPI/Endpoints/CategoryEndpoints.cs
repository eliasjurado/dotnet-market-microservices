using AutoMapper;
using FluentValidation;
using Market.Domain.Models.Dto.Services;
using Market.Services.ProductAPI.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace Market.Services.ProductAPI.Endpoints
{
    public static class CategoryEndpoints
    {
        public static void ConfigureCategoryEndpoints(this WebApplication app)
        {

            app.MapGet("/api/Category", GetAllCategory)
                .WithName("GetCategories")
                .Produces<ResponseDto<List<SelectListItem>>>(200)
                .Produces(401)
                .Produces(403)
                .RequireAuthorization();
        }

        private async static Task<IResult> GetAllCategory(HttpContext context, IConfiguration _configuration, ApplicationDbContext _db, IMapper _mapper, ILogger<Program> _logger)
        {
            ResponseDto<List<SelectListItem>> response = new();

            var userName = context.User.Claims.Where(c => c.Type.Equals(ClaimTypes.Name) || c.Type.Equals(ClaimTypes.Email)).Select(c => c.Value).SingleOrDefault();

            if (string.IsNullOrWhiteSpace(userName))
            {
                response.Message = "Categories not retrieved";
                response.Metadata.Add("Invalid User Name was received");
                return Results.BadRequest(response);
            }

            _logger.Log(LogLevel.Information, "Getting all Categories");
            response.IsSuccess = true;
            response.Message = "Categories retrieved";
            await _db.Categories.ForEachAsync(x => response.Data.Add(new SelectListItem { Value = x.Name, Text = x.Name }));
            response.StatusCode = HttpStatusCode.OK;
            response.Status = nameof(HttpStatusCode.OK);

            return Results.Ok(response);
        }

    }
}
