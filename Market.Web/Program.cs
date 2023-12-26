using Htmx.TagHelpers;
using Market.Infrastructure;
using Market.Web;
using Market.Web.Service;
using Market.Web.Service.IService;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<ICouponService, CouponService>();
builder.Services.AddHttpClient<IAuthService, AuthService>();

Base.CouponAPIBase = builder.Configuration.GetValue<string>("ServiceUrls:CouponAPI");
Base.AuthAPIBase = builder.Configuration.GetValue<string>("ServiceUrls:AuthAPI");

builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromHours(10);
            options.LoginPath = "/Auth/Index";
            options.AccessDeniedPath = "/Auth/AccessDenied";
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapHtmxAntiforgeryScript();
app.MapRazorPages();

app.Run();
