using fuel_consumption_tracker.Client.Authentication.Contracts;
using fuel_consumption_tracker.Client.Authentication.Implementations;
using fuel_consumption_tracker.Client.States;
using fuel_consumption_tracker.DAL;
using Ma.Marjane.GRAM.Domain.Models.Security;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DbContextSecurity>(options =>
    options.UseSqlServer(connectionString
    , b => b.MigrationsAssembly("fuel_consumption_tracker.DAL")
    ));


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();



builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<DbContextSecurity>()
    .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = false;
    options.SignIn.RequireConfirmedAccount = false;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.RequireUniqueEmail = true;
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
});

builder.Services.AddAuthorizationCore();

//builder.Services.AddAuthorizationCore(options =>
//{
//    options.AddPolicy("PolicyAddGrpChptr",
//        authBuilder =>
//        {
//            authBuilder.RequireClaim("BtnAddGrpChaptr");
//        });
//});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
