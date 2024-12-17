using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    public class AdminController : Controller
    {
        // GET: Admin/Dashboard
        public IActionResult Dashboard()
        {
            // Check if user is admin
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Admin")
            {
                return RedirectToAction("Login", "Auth");
            }
            
            return View();
        }

        // GET: Admin
        public IActionResult Index()
        {
            return RedirectToAction("Dashboard");
        }
    }
}
