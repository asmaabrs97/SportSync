using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SportSync.Models;
using System.Threading.Tasks;

namespace SportSync.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        // GET: User/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            // Check if user is logged in
            var userRole = HttpContext.Session.GetString("UserRole");
            if (string.IsNullOrEmpty(userRole))
            {
                return RedirectToAction("Login", "Auth");
            }

            // Get current user
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Login", "Auth");
            }

            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View(user);
        }

        // GET: User
        public IActionResult Index()
        {
            return RedirectToAction("Dashboard");
        }
    }
}