using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nettside.Data;
using Nettside.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Configure the database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MariaDbConnection"),
        new MySqlServerVersion(new Version(10, 5, 9))));

// Configure Identity
builder.Services.AddIdentity<Users, IdentityRole>(options =>
    {
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireUppercase = false;
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();