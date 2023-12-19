using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Final.Data;
using Final.Services;
using Final.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ClotheContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ClotheContext") ?? throw new InvalidOperationException("Connection string 'ClotheContext' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ClotheContext>();
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IClotheService, ClotheService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IRolesService, RolesService>();
builder.Services.AddScoped<IBrandService, BrandService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    
app.MapRazorPages();

app.Run();