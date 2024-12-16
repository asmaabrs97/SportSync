using System.ComponentModel.DataAnnotations;

namespace SportSync.Models.ViewModels
{
    public class PaymentMethodViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        [Required]
        [MaxLength(255)]
        public string Details { get; set; }

        public bool IsDefault { get; set; }
    }
}
