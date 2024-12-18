using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportSync.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        [MaxLength(20)]
        public string PostalCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string Country { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string EmergencyContact { get; set; }

        [Required]
        [Phone]
        public string EmergencyPhone { get; set; }

        [MaxLength(500)]
        public string MedicalInformation { get; set; }

        [MaxLength(255)]
        public string ProfilePictureUrl { get; set; }

        public DateTime? LastUpdated { get; set; }

        // Navigation property
        public virtual User User { get; set; }

        public UserProfile()
        {
            LastUpdated = DateTime.UtcNow;
        }
    }
}
