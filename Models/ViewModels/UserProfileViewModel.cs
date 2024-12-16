using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SportSync.Models.ViewModels
{
    public class UserProfileViewModel
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string EmergencyContact { get; set; }
        public string EmergencyPhone { get; set; }
        public string MedicalInformation { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }
}
