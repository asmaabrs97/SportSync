using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportSync.Models
{
    public class Session
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int SportId { get; set; }

        [Required]
        public int CoachId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [MaxLength(100)]
        public string Location { get; set; }

        public int MaxCapacity { get; set; }
        public int CurrentParticipants { get; set; }

        [Required]
        public string Description { get; set; }
        public bool IsCancelled { get; set; }
        public string CancellationReason { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual Sport Sport { get; set; }
        public virtual Coach Coach { get; set; }
        public ICollection<Registration> Registrations { get; set; }

        public Session()
        {
            IsCancelled = false;
            CurrentParticipants = 0;
        }
    }
}