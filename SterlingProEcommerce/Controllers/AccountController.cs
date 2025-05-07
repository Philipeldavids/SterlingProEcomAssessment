
using Core.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SterlingProEcommerce.Models.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SterlingProEcommerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenService _jwtTokenService;
        

        public AccountController(IUserService userService, IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
            _userService = userService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _userService.AuthenticateUser(model.Email, model.Password);
            if (result.Success)
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(result.Token);

                var claims = token.Claims.ToList(); // Use claims from token (e.g., name, role, etc.)

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Sign in the user
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                //ViewBag.Token = result.Token;
                HttpContext.Session.SetString("JWTToken", result.Token);

                // ✅ Check if the user is an admin
                //var roleClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                //if (roleClaim == "Admin")
                //{
                //    return RedirectToAction("Dashboard", "Home");
                //}

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("JWTToken");
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _userService.Register(model.FirstName, model.LastName, model.Email, model.Password);
            if(result == true)
            {
                TempData["Success"] = "Registration successful!";
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}
