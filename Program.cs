using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentACarProject.Data;
using RentACarProject.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<RentACarProject.Data.ApplicationContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDatabase")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationContext>();



builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();



app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
