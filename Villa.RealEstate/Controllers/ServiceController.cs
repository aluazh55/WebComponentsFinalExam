using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Villa.RealEstate.Controllers
{
    public class ServiceController : Controller
    {

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
