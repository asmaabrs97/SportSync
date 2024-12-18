using Microsoft.AspNetCore.Mvc;
using SportSync.Models.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using SportSync.Models;
using SportSync.Data; // Assuming your DbContext is in this namespace

namespace SportSync.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context; // Assuming your DbContext is named SportSyncContext

        public AuthController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IWebHostEnvironment environment,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _environment = environment;
            _context = context;
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
            try
            {
                // Get registration data from session
                var registrationDataString = HttpContext.Session.GetString("RegistrationData");
                var medicalDataString = HttpContext.Session.GetString("MedicalData");

                if (string.IsNullOrEmpty(registrationDataString) || string.IsNullOrEmpty(medicalDataString))
                {
                    ModelState.AddModelError(string.Empty, "Session data is missing. Please start registration again.");
                    return View("Register", new RegistrationStepsViewModel { CurrentStep = 1 });
                }

                var registrationData = JsonSerializer.Deserialize<RegistrationViewModel>(registrationDataString);
                var medicalData = JsonSerializer.Deserialize<MedicalInfoViewModel>(medicalDataString);

                if (registrationData == null || medicalData == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid registration data. Please start registration again.");
                    return View("Register", new RegistrationStepsViewModel { CurrentStep = 1 });
                }

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
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View("Register", new RegistrationStepsViewModel { CurrentStep = 3, Documents = model });
                }

                // Create the user profile with medical information
                var userProfile = new UserProfile
                {
                    UserId = user.Id,
                    MedicalInformation = medicalData.MedicalConditions,
                    EmergencyContact = medicalData.EmergencyContactName,
                    EmergencyPhone = medicalData.EmergencyContactPhone
                };
                _context.UserProfiles.Add(userProfile);

                // Save documents if any were uploaded
                if (model != null)
                {
                    var documentsPath = Path.Combine(_environment.WebRootPath, "uploads", user.Id);
                    Directory.CreateDirectory(documentsPath);

                    // Save each document if provided
                    if (model.ProofOfIdentity != null)
                    {
                        var docPath = await SaveDocument(model.ProofOfIdentity, "ProofOfIdentity", documentsPath);
                        if (!string.IsNullOrEmpty(docPath))
                        {
                            _context.Documents.Add(new Document
                            {
                                UserId = user.Id,
                                DocumentType = "ProofOfIdentity",
                                FilePath = docPath,
                                UploadDate = DateTime.UtcNow
                            });
                        }
                    }

                    if (model.MedicalCertificate != null)
                    {
                        var docPath = await SaveDocument(model.MedicalCertificate, "MedicalCertificate", documentsPath);
                        if (!string.IsNullOrEmpty(docPath))
                        {
                            _context.Documents.Add(new Document
                            {
                                UserId = user.Id,
                                DocumentType = "MedicalCertificate",
                                FilePath = docPath,
                                UploadDate = DateTime.UtcNow
                            });
                        }
                    }

                    if (model.InsuranceCertificate != null)
                    {
                        var docPath = await SaveDocument(model.InsuranceCertificate, "InsuranceCertificate", documentsPath);
                        if (!string.IsNullOrEmpty(docPath))
                        {
                            _context.Documents.Add(new Document
                            {
                                UserId = user.Id,
                                DocumentType = "InsuranceCertificate",
                                FilePath = docPath,
                                UploadDate = DateTime.UtcNow
                            });
                        }
                    }

                    if (model.LiabilityWaiverForm != null)
                    {
                        var docPath = await SaveDocument(model.LiabilityWaiverForm, "LiabilityWaiver", documentsPath);
                        if (!string.IsNullOrEmpty(docPath))
                        {
                            _context.Documents.Add(new Document
                            {
                                UserId = user.Id,
                                DocumentType = "LiabilityWaiver",
                                FilePath = docPath,
                                UploadDate = DateTime.UtcNow
                            });
                        }
                    }
                }

                await _context.SaveChangesAsync();

                // Add to regular user role
                var roleResult = await _userManager.AddToRoleAsync(user, "User");
                if (!roleResult.Succeeded)
                {
                    await _userManager.DeleteAsync(user);
                    ModelState.AddModelError(string.Empty, "Failed to assign user role. Please try again.");
                    return View("Register", new RegistrationStepsViewModel { CurrentStep = 3, Documents = model });
                }

                // Clear session data
                HttpContext.Session.Remove("RegistrationData");
                HttpContext.Session.Remove("MedicalData");

                // Sign in the user
                await _signInManager.SignInAsync(user, isPersistent: false);

                // Set success message and user session data
                TempData["SuccessMessage"] = "Registration completed successfully! Welcome to SportSync!";
                HttpContext.Session.SetString("UserRole", "User");
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserId", user.Id);

                // Redirect to dashboard with success message
                return RedirectToAction("Dashboard", "User");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred during registration. Please try again.");
                return View("Register", new RegistrationStepsViewModel { CurrentStep = 3, Documents = model });
            }
        }

        private async Task<string> SaveDocument(IFormFile file, string prefix, string path)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = $"{prefix}_{Path.GetFileName(file.FileName)}";
                var filePath = Path.Combine(path, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return filePath;
            }
            return null;
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
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email ?? string.Empty, 
                    model.Password, 
                    model.RememberMe, 
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user != null)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        var isAdmin = roles.Contains("Admin");
                        var isManager = roles.Contains("Manager");
                        
                        HttpContext.Session.SetString("UserId", user.Id);
                        HttpContext.Session.SetString("UserRole", isAdmin ? "Admin" : (isManager ? "Manager" : "User"));
                        HttpContext.Session.SetString("UserEmail", user.Email);

                        // Ensure the user has at least one role
                        if (!roles.Any())
                        {
                            // If it's the admin email but no role, add admin role
                            if (user.Email.ToLower() == "admin@sportsync.com")
                            {
                                await _userManager.AddToRoleAsync(user, "Admin");
                                HttpContext.Session.SetString("UserRole", "Admin");
                                return RedirectToAction("Dashboard", "Admin");
                            }
                            // Otherwise add user role
                            else
                            {
                                await _userManager.AddToRoleAsync(user, "User");
                                HttpContext.Session.SetString("UserRole", "User");
                            }
                        }

                        if (isAdmin)
                        {
                            return RedirectToAction("Dashboard", "Admin");
                        }
                        else if (isManager)
                        {
                            return RedirectToAction("Dashboard", "Manager");
                        }
                        return RedirectToLocal(returnUrl);
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == "Admin")
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            else if (userRole == "Manager")
            {
                return RedirectToAction("Dashboard", "Manager");
            }
            
            return RedirectToAction("Index", "Home");
        }

        // POST: /Auth/SignOut
        [HttpPost]
        [ValidateAntiForgeryToken]
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
