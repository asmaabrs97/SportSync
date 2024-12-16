using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportSync.Models
{
    public class Registration
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int SportId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public RegistrationStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool AutoRenew { get; set; }
        public string CancellationReason { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Sport Sport { get; set; }
        public virtual Payment Payment { get; set; }

        public Registration()
        {
            CreatedAt = DateTime.UtcNow;
            Status = RegistrationStatus.Pending;
        }
    }

    public enum RegistrationStatus
    {
        Pending,
        Active,
        Cancelled,
        Expired
    }
}
