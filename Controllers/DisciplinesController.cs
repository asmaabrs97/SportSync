using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    public class DisciplinesController : Controller
    {
        // GET: DisciplinesController
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SkiAlpin()
        {
            return View();
        }

        public IActionResult Football()
        {
            return View();
        }

        public IActionResult Basketball()
        {
            return View();
        }

        public IActionResult Handball()
        {
            return View();
        }

        public IActionResult Volleyball()
        {
            return View();
        }

        public IActionResult Baseball()
        {
            return View();
        }

        public IActionResult Cricket()
        {
            return View();
        }

        public IActionResult WaterPolo()
        {
            return View();
        }

        public IActionResult Details(string id)
        {
            return View();
        }

        public IActionResult SessionDetails(string discipline, string session)
        {
            ViewData["Discipline"] = discipline;
            ViewData["Session"] = session;
            return View();
        }
    }
}
