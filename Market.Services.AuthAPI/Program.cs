using Mango.Services.AuthAPI.Service;
using Market.Infrastructure;
using Market.Services.AuthAPI.Data;
using Market.Services.AuthAPI.Endpoints;
using Market.Services.AuthAPI.Models;
using Market.Services.AuthAPI.Service;
using Market.Services.AuthAPI.Service.IService;
using Market.Services.CouponAPI;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
//app.UseAuthorization();
app.ConfigureAuthEndpoints();
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
