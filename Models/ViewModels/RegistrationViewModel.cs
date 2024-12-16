using System.ComponentModel.DataAnnotations;

namespace SportSync.Models.ViewModels
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "You must be 14 years or older")]
        public bool IsOver14 { get; set; }

        [Required(ErrorMessage = "You must accept the terms of service")]
        public bool AcceptTerms { get; set; }
    }

    public class MedicalInfoViewModel
    {
        public string MedicalConditions { get; set; }
        public string Allergies { get; set; }
        public string DietaryRestrictions { get; set; }
        
        [Required(ErrorMessage = "Emergency contact name is required")]
        public string EmergencyContactName { get; set; }
        
        [Required(ErrorMessage = "Emergency contact phone is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string EmergencyContactPhone { get; set; }
        
        public string AdditionalNotes { get; set; }
    }

    public class DocumentsViewModel
    {
        [Required(ErrorMessage = "Proof of identity is required")]
        public IFormFile ProofOfIdentity { get; set; }

        [Required(ErrorMessage = "Medical certificate is required")]
        public IFormFile MedicalCertificate { get; set; }

        public IFormFile InsuranceCertificate { get; set; }

        [Required(ErrorMessage = "Liability waiver form is required")]
        public IFormFile LiabilityWaiverForm { get; set; }
    }
}
