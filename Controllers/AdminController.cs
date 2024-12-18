using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SportSync.Models;
using SportSync.Models.ViewModels;
using SportSync.Data;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;

namespace SportSync.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Admin/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            // Check if user is admin
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Admin")
            {
                // Double check with actual role from database
                var userEmail = HttpContext.Session.GetString("UserEmail");
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Contains("Admin"))
                    {
                        HttpContext.Session.SetString("UserRole", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Login", "Auth");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }

            var viewModel = new AdminDashboardViewModel
            {
                Users = await GetAllUsers(),
                Statistics = await GetRegistrationStatistics(),
                Disciplines = await GetDisciplines(),
                Sessions = await GetAllSessions()
            };

            return View(viewModel);
        }

        // GET: Admin/AddDiscipline
        public IActionResult AddDiscipline()
        {
            return View(new SportViewModel());
        }

        // POST: Admin/AddDiscipline
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDiscipline(SportViewModel model)
        {
            if (ModelState.IsValid)
            {
                string imageUrl = null;
                if (model.Image != null)
                {
                    // Save the image file and get its URL
                    var fileName = Path.GetRandomFileName() + Path.GetExtension(model.Image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "disciplines", fileName);
                    
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }
                    imageUrl = "/images/disciplines/" + fileName;
                }

                var sport = new Sport
                {
                    Name = model.Name,
                    Description = model.Description,
                    Requirements = model.Requirements,
                    Schedule = model.Schedule,
                    ImageUrl = imageUrl,
                    PricePerMonth = model.PricePerMonth,
                    MaxParticipants = model.MaxParticipants,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Sports.Add(sport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Dashboard));
            }
            return View(model);
        }

        // GET: Admin/EditDiscipline/5
        public async Task<IActionResult> EditDiscipline(int id)
        {
            var sport = await _context.Sports.FindAsync(id);
            if (sport == null)
            {
                return NotFound();
            }

            var viewModel = new SportViewModel
            {
                Id = sport.Id,
                Name = sport.Name,
                Description = sport.Description,
                Requirements = sport.Requirements,
                Schedule = sport.Schedule,
                ImageUrl = sport.ImageUrl,
                PricePerMonth = sport.PricePerMonth,
                MaxParticipants = sport.MaxParticipants
            };

            return View(viewModel);
        }

        // POST: Admin/EditDiscipline/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDiscipline(int id, SportViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var sport = await _context.Sports.FindAsync(id);
                if (sport == null)
                {
                    return NotFound();
                }

                if (model.Image != null)
                {
                    // Delete old image if it exists
                    if (!string.IsNullOrEmpty(sport.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", sport.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // Save the new image file and get its URL
                    var fileName = Path.GetRandomFileName() + Path.GetExtension(model.Image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "disciplines", fileName);
                    
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }
                    sport.ImageUrl = "/images/disciplines/" + fileName;
                }

                sport.Name = model.Name;
                sport.Description = model.Description;
                sport.Requirements = model.Requirements;
                sport.Schedule = model.Schedule;
                sport.PricePerMonth = model.PricePerMonth;
                sport.MaxParticipants = model.MaxParticipants;
                sport.UpdatedAt = DateTime.UtcNow;

                _context.Update(sport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Dashboard));
            }
            return View(model);
        }

        // GET: Admin/AddSession
        public async Task<IActionResult> AddSession()
        {
            ViewBag.Sports = await _context.Sports.ToListAsync();
            ViewBag.Coaches = await _context.Coaches.Include(c => c.User).ToListAsync();
            return View(new SessionViewModel());
        }

        // POST: Admin/AddSession
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSession(SessionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var session = new Session
                {
                    SportId = model.SportId,
                    CoachId = model.CoachId,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    Location = model.Location,
                    Description = model.Description,
                    MaxCapacity = model.MaxCapacity,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Sessions.Add(session);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Dashboard));
            }

            ViewBag.Sports = await _context.Sports.ToListAsync();
            ViewBag.Coaches = await _context.Coaches.Include(c => c.User).ToListAsync();
            return View(model);
        }

        // GET: Admin/EditSession/5
        public async Task<IActionResult> EditSession(int id)
        {
            var session = await _context.Sessions
                .Include(s => s.Sport)
                .Include(s => s.Coach)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (session == null)
            {
                return NotFound();
            }

            var viewModel = new SessionViewModel
            {
                Id = session.Id,
                SportId = session.SportId,
                CoachId = session.CoachId,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                Location = session.Location,
                Description = session.Description,
                MaxCapacity = session.MaxCapacity
            };

            ViewBag.Sports = await _context.Sports.ToListAsync();
            ViewBag.Coaches = await _context.Coaches.Include(c => c.User).ToListAsync();
            return View(viewModel);
        }

        // POST: Admin/EditSession/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSession(int id, SessionViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var session = await _context.Sessions.FindAsync(id);
                if (session == null)
                {
                    return NotFound();
                }

                session.SportId = model.SportId;
                session.CoachId = model.CoachId;
                session.StartTime = model.StartTime;
                session.EndTime = model.EndTime;
                session.Location = model.Location;
                session.Description = model.Description;
                session.MaxCapacity = model.MaxCapacity;
                session.UpdatedAt = DateTime.UtcNow;

                _context.Update(session);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Dashboard));
            }

            ViewBag.Sports = await _context.Sports.ToListAsync();
            ViewBag.Coaches = await _context.Coaches.Include(c => c.User).ToListAsync();
            return View(model);
        }

        // POST: Admin/DeleteSession/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSession(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Dashboard));
        }

        // POST: Admin/DeleteUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["Message"] = "User deleted successfully.";
                return RedirectToAction(nameof(Dashboard));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return RedirectToAction(nameof(Dashboard));
        }

        // POST: Admin/ToggleDisciplineStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleDisciplineStatus(int id)
        {
            var sport = await _context.Sports.FindAsync(id);
            if (sport == null)
            {
                return NotFound();
            }

            sport.IsActive = !sport.IsActive;
            sport.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            TempData["Message"] = $"Discipline {(sport.IsActive ? "activated" : "deactivated")} successfully.";
            return RedirectToAction(nameof(Dashboard));
        }

        // POST: Admin/DeleteDiscipline
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDiscipline(int id)
        {
            var sport = await _context.Sports.FindAsync(id);
            if (sport == null)
            {
                return NotFound();
            }

            _context.Sports.Remove(sport);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Discipline deleted successfully.";
            return RedirectToAction(nameof(Dashboard));
        }

        private async Task<List<UserViewModel>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Role = roles.FirstOrDefault(),
                    IsActive = user.IsActive
                });
            }

            return userViewModels;
        }

        private async Task<RegistrationStatistics> GetRegistrationStatistics()
        {
            var users = await _userManager.Users.ToListAsync();
            var registrations = await _context.Registrations.ToListAsync();

            var stats = new RegistrationStatistics
            {
                TotalUsers = users.Count,
                ActiveUsers = users.Count(u => u.IsActive),
                RegistrationsPerDiscipline = registrations
                    .GroupBy(r => r.SportId)
                    .ToDictionary(
                        g => _context.Sports.Find(g.Key)?.Name ?? "Unknown",
                        g => g.Count()
                    ),
                RegistrationsPerMonth = registrations
                    .GroupBy(r => r.CreatedAt.ToString("MMMM yyyy"))
                    .ToDictionary(g => g.Key, g => g.Count())
            };

            return stats;
        }

        private async Task<List<DisciplineViewModel>> GetDisciplines()
        {
            var disciplines = await _context.Sports
                .Select(d => new DisciplineViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    ActiveSessions = _context.Sessions.Count(s => s.SportId == d.Id && s.EndTime >= DateTime.Now),
                    RegisteredUsers = _context.Registrations.Count(r => r.SportId == d.Id)
                })
                .ToListAsync();

            return disciplines;
        }

        private async Task<List<SessionViewModel>> GetAllSessions()
        {
            var sessions = await _context.Sessions
                .Include(s => s.Sport)
                .Include(s => s.Coach)
                    .ThenInclude(c => c.User)
                .Select(s => new SessionViewModel
                {
                    Id = s.Id,
                    SportId = s.SportId,
                    SportName = s.Sport.Name,
                    CoachId = s.CoachId,
                    CoachName = $"{s.Coach.User.FirstName} {s.Coach.User.LastName}",
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    Location = s.Location,
                    Description = s.Description,
                    MaxCapacity = s.MaxCapacity,
                    CurrentParticipants = s.CurrentParticipants
                })
                .ToListAsync();

            return sessions;
        }

        // GET: Admin
        public IActionResult Index()
        {
            return RedirectToAction("Dashboard");
        }
    }
}
