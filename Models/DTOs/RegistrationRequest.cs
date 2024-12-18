using System.ComponentModel.DataAnnotations;

namespace SportSync.Models.DTOs
{
    public class RegistrationRequest
    {
        [Required]
        public int SportId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string PaymentMethodId { get; set; }
        public bool AutoRenew { get; set; }
    }
}
