using Market.Infrastructure;
using Market.Services.AuthAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
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
