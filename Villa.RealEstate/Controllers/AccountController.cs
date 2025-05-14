using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Villa.RealEstate.Models;

namespace Villa.RealEstate.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private TokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            Login login = new Login();
            login.ReturnUrl = returnUrl;
            return View(login);
        }



        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            AppUser user = await _userManager.FindByEmailAsync(login.Email);
            if (user != null)
            {
                await _signInManager.SignOutAsync();
                var result = await _signInManager
                    .PasswordSignInAsync(user, login.Password, false, false);

                if (result.Succeeded)
                {
                    var jwtToken = await _tokenService.GenerateAccessToken(user);
                    Response.Cookies.Append("jwtToken", jwtToken);

                    return RedirectToAction("Index", "Home");
                }

            }
            return View(login);
        }
    }
}
