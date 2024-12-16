using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SportSync.Models.ViewModels
{
    public class SportViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal PricePerMonth { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MaxParticipants { get; set; }

        public string Requirements { get; set; }
        public string Schedule { get; set; }
        public IFormFile Image { get; set; }
    }
}
