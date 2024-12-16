using Microsoft.AspNetCore.Mvc;
using SportSync.Models.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace MyApp.Namespace
{
    public class AuthController : Controller
    {
        // GET: /Auth/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Auth/Register
        [HttpPost]
        public IActionResult Register(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Store registration data in Session
            HttpContext.Session.SetString("RegistrationData", JsonSerializer.Serialize(model));
            return RedirectToAction("MedicalInfo");
        }

        // GET: /Auth/MedicalInfo
        public IActionResult MedicalInfo()
        {
            var registrationData = HttpContext.Session.GetString("RegistrationData");
            if (string.IsNullOrEmpty(registrationData))
            {
                return RedirectToAction("Register");
            }
            return View();
        }

        // POST: /Auth/MedicalInfo
        [HttpPost]
        public IActionResult MedicalInfo(MedicalInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Store medical info in Session
            HttpContext.Session.SetString("MedicalInfo", JsonSerializer.Serialize(model));
            return RedirectToAction("Documents");
        }

        // GET: /Auth/Documents
        public IActionResult Documents()
        {
            var medicalInfo = HttpContext.Session.GetString("MedicalInfo");
            if (string.IsNullOrEmpty(medicalInfo))
            {
                return RedirectToAction("MedicalInfo");
            }
            return View();
        }

        // POST: /Auth/Documents
        [HttpPost]
        public async Task<IActionResult> Documents(DocumentsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Process file uploads
                var documentsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "documents");
                Directory.CreateDirectory(documentsPath);

                // Save files with unique names
                if (model.ProofOfIdentity != null)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProofOfIdentity.FileName;
                    var filePath = Path.Combine(documentsPath, uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ProofOfIdentity.CopyToAsync(stream);
                    }
                }

                // Similar process for other documents...

                // Create user account and save all information
                // Get the stored registration and medical data
                var registrationData = HttpContext.Session.GetString("RegistrationData");
                var medicalInfo = HttpContext.Session.GetString("MedicalInfo");

                if (!string.IsNullOrEmpty(registrationData) && !string.IsNullOrEmpty(medicalInfo))
                {
                    var registration = JsonSerializer.Deserialize<RegistrationViewModel>(registrationData);
                    var medical = JsonSerializer.Deserialize<MedicalInfoViewModel>(medicalInfo);

                    // TODO: Implement user creation logic using registration and medical data
                }

                // Clear session data
                HttpContext.Session.Clear();

                return RedirectToAction("RegistrationComplete");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error uploading documents. Please try again.");
                return View(model);
            }
        }

        // GET: /Auth/RegistrationComplete
        public IActionResult RegistrationComplete()
        {
            return View();
        }

        // GET: /Auth/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Auth/Login
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // TODO: Implement login logic
            return RedirectToAction("Index", "Home");
        }
    }
}
