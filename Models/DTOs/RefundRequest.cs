using System.ComponentModel.DataAnnotations;

namespace SportSync.Models.DTOs
{
    public class RefundRequest
    {
        [Required]
        public string PaymentId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Reason { get; set; }
    }
}
