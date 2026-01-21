using Identity.Api.Model;
using Identity.Domain.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Identity.Api.Controllers;

public class AccountController(IUserRepository userRepository) : Controller
{
    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
    {
        var user = await userRepository.GetUserByLoginAsync("", "");

        if (user == null)
        {
            ModelState.AddModelError("", "Invalid username or password");
            return View(model);
        }

        var claims = new List<Claim>
        {
            new("sub", user.Subject),
            new("email", user.Email),
        };
        
        claims.AddRange(user.Roles.Select(claim => new Claim("role", claim)));

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = false,
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),authProperties);

        return Redirect(Url.IsLocalUrl(returnUrl) ? returnUrl : "/");
    }
}