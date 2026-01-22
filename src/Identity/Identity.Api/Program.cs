using Identity.Api.Contexts;
using Identity.Api.Handler.Exception;
using Identity.Application.Commands.GetAuthorizeCode;
using Identity.Application.Interfaces;
using Identity.Domain.Abstractions;
using Identity.Infrastructure.Implements;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/account/login";
        options.LogoutPath = "/account/logout";
        options.AccessDeniedPath = "/auth/denied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    
        options.Cookie.Name = "Identity.Auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });
builder.Services.AddHttpContextAccessor();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddMediatR(ops =>
{
    ops.RegisterServicesFromAssembly(typeof(GetAuthorizeCodeCommand).Assembly);
});

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICurrentUser, CurrentUserContext>();

var app = builder.Build();
app.UseExceptionHandler();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.MapDefaultControllerRoute();

app.MapControllers();

app.Run();