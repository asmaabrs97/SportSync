using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportSync.Models
{
    public class Coach
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Specialization { get; set; }

        [Required]
        [MaxLength(500)]
        public string Qualifications { get; set; }

        [MaxLength(1000)]
        public string Biography { get; set; }

        [Required]
        [Range(0, 50)]
        public int YearsOfExperience { get; set; }

        public DateTime JoinDate { get; set; }

        public bool IsActive { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }

        public Coach()
        {
            JoinDate = DateTime.UtcNow;
            IsActive = true;
            Sessions = new HashSet<Session>();
        }
    }
}
