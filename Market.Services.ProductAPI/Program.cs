using FluentValidation;
using Market.Domain.Models;
using Market.Infrastructure;
using Market.Services.ProductAPI;
using Market.Services.ProductAPI.Data;
using Market.Services.ProductAPI.Endpoints;
using Market.Services.ProductAPI.Repository;
using Market.Services.ProductAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = Base.AuthorizationCookie,
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = Base.BearerReferenceType,
        Description =
        "<b>JWT Authorization header using Bearer scheme</b>\n\r\n\r" +
        "Enter 'Bearer' [space] and then your token in the text input below.\n\r\n\r" +
        "Example: `Bearer Generated-JWT-Token`\n\r\n\r"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = Base.BearerReferenceType
                },
                Scheme = "oauth2",
                Name = Base.BearerReferenceType,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var jwtOptions = builder.Configuration.GetSection("ApiSettings:JwtOptions").Get<JwtOptions>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Secret)),
        ValidateIssuer = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtOptions.Audience,
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.ConfigureProductEndpoints();
app.ConfigureCategoryEndpoints();

app.UseHttpsRedirection();

ApplyPendingMigration();

app.Run();

async void ApplyPendingMigration()
{
    try
    {
        using (var scope = app.Services.CreateScope())
        {
            var _db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if ((await _db.Database.GetPendingMigrationsAsync()).Any())
            {
                _db.Database.Migrate();
            }
        }
    }
    catch (Exception ex)
    {
        Format.GetInnerExceptionMessage(ex).ToList().ForEach(Console.WriteLine);
    }
}