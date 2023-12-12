using Market.Web.Models.Dto;
using Market.Web.Service;
using Market.Web.Service.IService;
using Market.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IService<CouponDto>, CouponService>();
Constants.CouponAPIBase = builder.Configuration.GetValue<string>("ServiceUrls:CouponAPI");
builder.Services.AddScoped<IBaseService<object>, BaseService>();
builder.Services.AddScoped<IService<CouponDto>, CouponService>();

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
