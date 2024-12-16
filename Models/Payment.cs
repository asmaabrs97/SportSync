using System;
using System.ComponentModel.DataAnnotations;

namespace SportSync.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int RegistrationId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public PaymentStatus Status { get; set; }

        [Required]
        public string TransactionId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? ProcessedAt { get; set; }

        public string PaymentMethod { get; set; }

        public string FailureReason { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Registration Registration { get; set; }

        public Payment()
        {
            CreatedAt = DateTime.UtcNow;
            Status = PaymentStatus.Pending;
        }
    }

    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded
    }
}
