using System.ComponentModel.DataAnnotations;

namespace SportSync.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        [Required]
        [MaxLength(500)]
        public string Details { get; set; }

        public bool IsDefault { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastUsed { get; set; }

        public bool IsActive { get; set; }

        // Navigation properties
        public virtual User User { get; set; }

        public PaymentMethod()
        {
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }
    }
}
