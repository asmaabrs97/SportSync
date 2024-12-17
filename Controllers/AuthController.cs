using Microsoft.AspNetCore.Mvc;
using SportSync.Models.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using SportSync.Models;

namespace SportSync.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment _environment;

        public AuthController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _environment = environment;
        }

        // GET: /Auth/Register
        public IActionResult Register(int step = 1)
        {
            var model = new RegistrationStepsViewModel
            {
                CurrentStep = step,
                Authentication = new RegistrationViewModel(),
                MedicalInfo = new MedicalInfoViewModel(),
                Documents = new DocumentsViewModel()
            };

            // If we're not on step 1, check if we have the previous step's data
            if (step > 1 && string.IsNullOrEmpty(HttpContext.Session.GetString("RegistrationData")))
            {
                return RedirectToAction(nameof(Register), new { step = 1 });
            }

            return View(model);
        }

        // POST: /Auth/Register (Step 1)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Store registration data in session
                HttpContext.Session.SetString("RegistrationData", JsonSerializer.Serialize(model));
                
                // Proceed to step 2
                return RedirectToAction(nameof(Register), new { step = 2 });
            }

            return View(new RegistrationStepsViewModel
            {
                CurrentStep = 1,
                Authentication = model
            });
        }

        // POST: /Auth/SaveMedicalInfo (Step 2)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveMedicalInfo(MedicalInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Store medical info in session
                HttpContext.Session.SetString("MedicalData", JsonSerializer.Serialize(model));
                
                // Proceed to step 3
                return RedirectToAction(nameof(Register), new { step = 3 });
            }

            return View("Register", new RegistrationStepsViewModel
            {
                CurrentStep = 2,
                MedicalInfo = model
            });
        }

        // POST: /Auth/SaveDocuments (Step 3)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveDocuments(DocumentsViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Get registration data from session
                var registrationData = JsonSerializer.Deserialize<RegistrationViewModel>(
                    HttpContext.Session.GetString("RegistrationData"));
                var medicalData = JsonSerializer.Deserialize<MedicalInfoViewModel>(
                    HttpContext.Session.GetString("MedicalData"));

                // Create the user
                var user = new User
                {
                    UserName = registrationData.Email,
                    Email = registrationData.Email,
                    FirstName = registrationData.FullName.Split(' ')[0],
                    LastName = registrationData.FullName.Split(' ').Length > 1 ? 
                        string.Join(" ", registrationData.FullName.Split(' ').Skip(1)) : "",
                    PhoneNumber = registrationData.PhoneNumber,
                    DateOfBirth = registrationData.DateOfBirth,
                    Gender = registrationData.Gender,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(user, registrationData.Password);
                if (result.Succeeded)
                {
                    // Save documents
                    var documentsPath = Path.Combine(_environment.WebRootPath, "uploads", user.Id);
                    Directory.CreateDirectory(documentsPath);

                    await SaveDocument(model.ProofOfIdentity, "ProofOfIdentity", documentsPath);
                    await SaveDocument(model.MedicalCertificate, "MedicalCertificate", documentsPath);
                    await SaveDocument(model.InsuranceCertificate, "InsuranceCertificate", documentsPath);
                    await SaveDocument(model.LiabilityWaiverForm, "LiabilityWaiver", documentsPath);

                    // Add to regular user role
                    await _userManager.AddToRoleAsync(user, "User");

                    // Clear session data
                    HttpContext.Session.Remove("RegistrationData");
                    HttpContext.Session.Remove("MedicalData");

                    // Set session data for logged-in user
                    HttpContext.Session.SetString("UserRole", "User");
                    HttpContext.Session.SetString("UserEmail", user.Email);

                    // Sign in
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Dashboard", "User");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View("Register", new RegistrationStepsViewModel
            {
                CurrentStep = 3,
                Documents = model
            });
        }

        private async Task SaveDocument(IFormFile file, string prefix, string path)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = $"{prefix}_{Path.GetFileName(file.FileName)}";
                var filePath = Path.Combine(path, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
        }

        // GET: /Auth/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user == null)
                    {
                        ModelState.AddModelError(string.Empty, "User not found.");
                        return View(model);
                    }

                    var roles = await _userManager.GetRolesAsync(user);
                    var role = roles.FirstOrDefault() ?? "User";
                    
                    HttpContext.Session.SetString("UserRole", role);
                    HttpContext.Session.SetString("UserEmail", user.Email);

                    if (role == "Admin")
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }
                    return RedirectToAction("Dashboard", "User");
                }

                if (result.IsLockedOut)
                {
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);
        }

        // POST: /Auth/SignOut
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
