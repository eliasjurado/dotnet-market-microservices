using AutoMapper;
using FluentValidation;
using Market.Domain.Models;
using Market.Domain.Models.Dto.Services;
using Market.Domain.Models.Dto.Services.Product;
using Market.Infrastructure;
using Market.Services.ProductAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Market.Services.ProductAPI.Endpoints
{
    public static class ProductEndpoints
    {
        public static void ConfigureProductEndpoints(this WebApplication app)
        {
            app.MapGet("/api/Product", GetAllProduct)
                .WithName("GetProducts")
                .Produces<ResponseDto<List<ProductDto>>>(200)
                .Produces(400);
            //.Produces(401)
            //.Produces(403)
            //.RequireAuthorization();

            app.MapGet("/api/Product/{id}", GetProduct)
                .WithName("GetProduct")
                .AddEndpointFilter(async (context, next) =>
                {
                    ResponseDto<object> response = new();
                    var id = context.GetArgument<string>(4);
                    int output;
                    if (!int.TryParse(id, out output))
                    {
                        response.Message = "Get Product Failed";
                        response.Metadata.Add("Invalid Id was received");
                        return Results.BadRequest(response);
                    }
                    if (output == 0)
                    {
                        response.Message = "Get Product Failed";
                        response.Metadata.Add("Id received cannot be zero");
                        return Results.BadRequest(response);
                    }
                    return await next(context);
                })
                .Produces<ResponseDto<ProductDto>>(200)
                .Produces(400);
            //.Produces(401)
            //.Produces(403)
            //.RequireAuthorization()

            app.MapPost("/api/Product", CreateProduct)
                .WithName("CreateProduct")
                .Accepts<ProductRequestDto>("application/json")
                .Produces<ResponseDto<ProductDto>>(201)
                .Produces(400)
                .Produces(401)
                .Produces(403)
                .RequireAuthorization("AdminOnly");

            app.MapPut("/api/Product/{id}", UpdateProduct)
                .WithName("UpdateProduct")
                .Accepts<ProductRequestDto>("application/json")
                .Produces<ResponseDto<ProductDto>>(200)
                .Produces(400)
                .Produces(401)
                .Produces(403)
                .RequireAuthorization("AdminOnly");

            app.MapDelete("/api/Product/{id}", DeleteProduct)
                .WithName("DeleteProduct")
                .Produces<ResponseDto<ProductDto>>(200)
                .Produces(400)
                .Produces(401)
                .Produces(403)
                .RequireAuthorization("AdminOnly");
        }

        private async static Task<IResult> GetAllProduct(HttpContext context, IConfiguration _configuration, IProductRepository _repository, IMapper _mapper, ILogger<Program> _logger)
        {
            ResponseDto<List<ProductDto>> response = new();
            //response.Message = "Get All Products Failed";

            //var userName = context.User.Claims.Where(c => c.Type.Equals(ClaimTypes.Name) || c.Type.Equals(ClaimTypes.Email)).Select(c => c.Value).SingleOrDefault();

            //if (string.IsNullOrWhiteSpace(userName))
            //{
            //    response.Metadata.Add("Invalid User Name was received");
            //    return Results.BadRequest(response);
            //}

            _logger.Log(LogLevel.Information, "Getting all Products");
            response.IsSuccess = true;
            response.Message = "Get All Products Succedeed";
            response.Metadata.Add("Products were retrieved successfully");
            response.Data = (await _repository.GetAsync()).Select(_mapper.Map<ProductDto>).ToList();
            response.StatusCode = HttpStatusCode.OK;
            response.Status = nameof(HttpStatusCode.OK);

            return Results.Ok(response);
        }

        //[Authorize(Roles = "Admin,Manager")]
        private async static Task<IResult> GetProduct(HttpContext context, IProductRepository _repository, IMapper _mapper, ILogger<Program> _logger, string id)
        {
            ResponseDto<ProductDto> response = new();
            response.Message = "Get Product Failed";

            //var userName = context.User.Claims.Where(c => c.Type.Equals(ClaimTypes.Name) || c.Type.Equals(ClaimTypes.Email)).Select(c => c.Value).SingleOrDefault();

            //if (string.IsNullOrWhiteSpace(userName))
            //{
            //    response.Metadata.Add("Invalid User Name was received");
            //    return Results.BadRequest(response);
            //}

            int output;
            if (!int.TryParse(id, out output))
            {
                response.Metadata.Add("Invalid Id was received");
                return Results.BadRequest(response);
            }

            var ProductDto = _mapper.Map<ProductDto>(await _repository.GetAsync(output));
            if (ProductDto == null)
            {
                response.Metadata.Add($"Product with Id {id} not found");
                return Results.BadRequest(response);
            }

            response.IsSuccess = true;
            response.Message = "Get Product Succeeded";
            response.Metadata.Add($"Product {ProductDto.Name} was retrieved successfully");
            response.Data = ProductDto;
            response.StatusCode = HttpStatusCode.OK;
            response.Status = nameof(HttpStatusCode.OK);

            return Results.Ok(response);
        }

        private async static Task<IResult> CreateProduct(HttpContext context, IProductRepository _repository, IMapper _mapper, ILogger<Program> _logger, IValidator<ProductRequestDto> _validator, [FromBody] ProductRequestDto ProductRequestDto)
        {
            var date = DateTime.Now;
            ResponseDto<ProductDto> response = new();
            response.Message = "Product Creation Failed";

            var userName = context.User.Claims.Where(c => c.Type.Equals(ClaimTypes.Name) || c.Type.Equals(ClaimTypes.Email)).Select(c => c.Value).SingleOrDefault();

            if (string.IsNullOrWhiteSpace(userName))
            {
                response.Metadata.Add("Invalid User Name was received");
                return Results.BadRequest(response);
            }

            var validationResult = await _validator.ValidateAsync(ProductRequestDto);

            if (!validationResult.IsValid)
            {
                validationResult.Errors.ForEach(x => response.Metadata.Add(x.ErrorMessage));
                return Results.BadRequest(response);
            }

            if (await _repository.GetAsync(ProductRequestDto.Name) != null)
            {

                response.Metadata.Add("Product Name already exists");
                return Results.BadRequest(response);
            }

            var Product = _mapper.Map<Product>(ProductRequestDto);
            Product.Name = ProductRequestDto.Name;
            Product.ProductCategoryName = ProductRequestDto.ProductCategoryName;
            Product.Description = ProductRequestDto.Description;
            Product.ProductPrice = ProductRequestDto.ProductPrice;
            Product.ProductImageUrl = ProductRequestDto.ProductImageUrl;
            Product.CreatedBy = userName;
            Product.UpdatedBy = userName;

            await _repository.CreateAsync(Product);
            await _repository.SaveAsync();

            response.IsSuccess = true;
            response.Message = "Product Creation Succeeded";
            response.Metadata.Add($"Product {Product.Name} was created successfully");
            response.Data = _mapper.Map<ProductDto>(Product);
            response.StatusCode = HttpStatusCode.Created;
            response.Status = Format.GetName(nameof(HttpStatusCode.Created));

            return Results.CreatedAtRoute("GetProduct", new { id = Product.ProductId }, response);
        }

        private async static Task<IResult> UpdateProduct(HttpContext context, IProductRepository _repository, IMapper _mapper, ILogger<Program> _logger, IValidator<ProductRequestDto> _validator, [FromBody] ProductRequestDto ProductRequestDto, string id)
        {
            ResponseDto<ProductDto> response = new();
            response.Message = "Update Product Failed";

            var userName = context.User.Claims.Where(c => c.Type.Equals(ClaimTypes.Name) || c.Type.Equals(ClaimTypes.Email)).Select(c => c.Value).SingleOrDefault();

            if (string.IsNullOrWhiteSpace(userName))
            {
                response.Metadata.Add("Invalid User Name was received");
                return Results.BadRequest(response);
            }

            var validationResult = await _validator.ValidateAsync(ProductRequestDto);
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

            var existingProduct = await _repository.GetAsync(ProductRequestDto.Name);
            if (existingProduct != null && existingProduct.ProductId != output)
            {
                response.Metadata.Add("Product Name already exists");
                return Results.BadRequest(response);
            }

            var Product = await _repository.GetAsync(output);

            if (Product == null)
            {
                response.Metadata.Add($"Product with Id {id} not found");
                return Results.BadRequest(response);
            }

            Product.Name = ProductRequestDto.Name;
            Product.ProductCategoryName = ProductRequestDto.ProductCategoryName;
            Product.Description = ProductRequestDto.Description;
            Product.ProductPrice = ProductRequestDto.ProductPrice;
            Product.ProductImageUrl = ProductRequestDto.ProductImageUrl;
            Product.UpdatedBy = userName;
            Product.UpdatedAt = DateTime.Now;

            await _repository.UpdateAsync(Product);
            await _repository.SaveAsync();

            response.IsSuccess = true;
            response.Message = "Update Product Succeeded";
            response.Metadata.Add($"Product {Product.Name} was updated successfully");
            response.Data = _mapper.Map<ProductDto>(Product);
            response.StatusCode = HttpStatusCode.OK;
            response.Status = nameof(HttpStatusCode.OK);

            return Results.Ok(response);
        }

        private async static Task<IResult> DeleteProduct(HttpContext context, IProductRepository _repository, IMapper _mapper, ILogger<Program> _logger, string id)
        {
            ResponseDto<ProductDto> response = new();
            response.Message = "Delete Product Failed";

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

            var Product = await _repository.GetAsync(output);

            if (Product == null)
            {
                response.Metadata.Add($"Product with Id {id} not found");
                return Results.BadRequest(response);
            }

            response.Data = _mapper.Map<ProductDto>(Product);
            await _repository.RemoveAsync(Product);
            await _repository.SaveAsync();
            response.IsSuccess = true;
            response.Message = "Delete Product Succeeded";
            response.Metadata.Add($"Product {Product.Name} was deleted successfully");
            response.StatusCode = HttpStatusCode.OK;
            response.Status = nameof(HttpStatusCode.OK);

            return Results.Ok(response);
        }
    }
}
