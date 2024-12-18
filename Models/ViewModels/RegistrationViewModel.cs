using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SportSync.Models.ViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "I am 14 years or older")]
        public bool IsOver14 { get; set; }

        [Required]
        [Display(Name = "I agree to the Terms of Service and Privacy Policy")]
        public bool AcceptTerms { get; set; }
    }

    public class MedicalInfoViewModel
    {
        [Display(Name = "Medical Conditions")]
        public string MedicalConditions { get; set; }

        [Display(Name = "Allergies")]
        public string Allergies { get; set; }

        [Display(Name = "Dietary Restrictions")]
        public string DietaryRestrictions { get; set; }

        [Required]
        [Display(Name = "Emergency Contact Name")]
        public string EmergencyContactName { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Emergency Contact Phone")]
        public string EmergencyContactPhone { get; set; }

        [Display(Name = "Additional Notes")]
        public string AdditionalNotes { get; set; }
    }

    public class DocumentsViewModel
    {
        [Display(Name = "Proof of Identity")]
        public IFormFile ProofOfIdentity { get; set; }

        [Display(Name = "Medical Certificate")]
        public IFormFile MedicalCertificate { get; set; }

        [Display(Name = "Insurance Certificate")]
        public IFormFile InsuranceCertificate { get; set; }

        [Display(Name = "Liability Waiver Form")]
        public IFormFile LiabilityWaiverForm { get; set; }
    }

    public class RegistrationStepsViewModel
    {
        public RegistrationViewModel Authentication { get; set; }
        public MedicalInfoViewModel MedicalInfo { get; set; }
        public DocumentsViewModel Documents { get; set; }
        public int CurrentStep { get; set; }
    }
}
