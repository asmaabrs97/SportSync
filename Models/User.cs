using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SportSync.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }

        // Navigation properties
        public virtual UserProfile Profile { get; set; }
        public virtual ICollection<Registration> Registrations { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Document> Documents { get; set; }

        public User()
        {
            Registrations = new HashSet<Registration>();
            Payments = new HashSet<Payment>();
            Documents = new HashSet<Document>();
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }
    }
}