using Asm2_1670.Data;
using Asm2_1670.Repository;
using Asm2_1670.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Asm2_1670.Utility;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));

builder.Services.AddIdentity<Asm2_1670.Models.User, IdentityRole>().AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddRazorPages();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.ConfigureApplicationCookie(option =>
{
	option.AccessDeniedPath = $"/Identity/Account/AccessDenied";
	option.LoginPath = $"/Identity/Account/Login";
	option.LogoutPath = $"/Identity/Account/Logout";
});

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

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
	name: "default",
	pattern: "{area=User}/{controller=Home}/{action=Index}/{id?}");

app.Run();
