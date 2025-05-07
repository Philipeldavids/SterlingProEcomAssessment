
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SterlingProEcommerce.Models.ViewModel;

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
                ViewBag.Token = result.Token;
                return RedirectToAction("Index", "Home", result.Token);
            }
            return View();
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
