using Admin.Villa.RealEstate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Admin.Villa.RealEstate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _db;

        public HomeController(ILogger<HomeController> logger,
            AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }



        public IActionResult JobPosition()
        {
            var positions = _db.JobPositions.ToList();
            return View(positions);
        }

        public IActionResult EditJobPosition(int id)
        {
            if (id != 0)
            {
                JobPosition position = _db.JobPositions.Find(id);
                return View(position);
            }
            else
            {
                return View(new JobPosition());
            }
        }

        [HttpPost]
        public IActionResult EditJobPosition(JobPosition position)
        {
            if (position.Id != 0)
            {
                var _position = _db.JobPositions.Find(position.Id);
                _position.Title = position.Title;
                _position.Description = position.Description;

                _db.SaveChanges();

            }
            else
            {
                

                _db.JobPositions.Add(position);
                _db.SaveChanges();
            }
            return RedirectToAction("JobPosition", "Home");
        }

        public ActionResult DeleteJobPosition(int id)
        {
            var _position = _db.JobPositions.Find(id);
            if (_position != null)
            {
                _db.JobPositions.Remove(_position);
                _db.SaveChanges();

            }
            return RedirectToAction("JobPosition", "Home");
        }



        




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
