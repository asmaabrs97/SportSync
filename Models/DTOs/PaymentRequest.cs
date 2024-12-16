using System.ComponentModel.DataAnnotations;

namespace SportSync.Models.DTOs
{
    public class PaymentRequest
    {
        [Required]
        public int RegistrationId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string PaymentMethodId { get; set; }

        public string Description { get; set; }
    }
}
