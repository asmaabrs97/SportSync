using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportSync.Models
{
    public class Sport
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal PricePerMonth { get; set; }

        public int MaxParticipants { get; set; }
        public string Requirements { get; set; }
        public string Schedule { get; set; }
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual ICollection<Registration> Registrations { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<Coach> Coaches { get; set; }

        public Sport()
        {
            Registrations = new HashSet<Registration>();
            Sessions = new HashSet<Session>();
            Coaches = new HashSet<Coach>();
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }
    }
}