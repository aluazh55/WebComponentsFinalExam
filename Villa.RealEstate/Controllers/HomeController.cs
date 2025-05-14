using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Villa.RealEstate.AppFilter;
using Villa.RealEstate.Models;

namespace Villa.RealEstate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<AppUser> _userManager;
        public HomeController(ILogger<HomeController> logger,
            UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        // [IEFilter]
       

        public async Task<IActionResult> IndexAsync()
        {
            _logger.LogTrace("Logging Trace Message");
            _logger.LogDebug("Logging Debug Message");
            _logger.LogInformation("Logging Information Message");
            _logger.LogWarning("Logging Warning Message");
            _logger.LogError("Logging Error Message");
            _logger.LogCritical("Logging Critical Message");

            AppUser user = new AppUser();
            user.UserName = "admin";
            user.Email = "aluazarkynbaeva@gmail.com";

            var result = await _userManager.CreateAsync(user, "Gg110188@");


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]


        public IActionResult Error(string message)
        {
            return View("Error", message);
        }

        public IActionResult Service()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                }
                );
            return Json(culture);
        }


        [HttpPost]
        //public IActionResult SendMessage(string name, string email, string message)
        public IActionResult SendMessage(Message userMessage)
        {
            var data = Request.Form;
            ///TODO write to db

            return View();
        }


        
    }
}
