using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SportSync.Models.ViewModels
{
    public class SportViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Description")]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Price per Month")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal PricePerMonth { get; set; }

        [Required]
        [Display(Name = "Requirements")]
        public string Requirements { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Schedule")]
        [StringLength(500)]
        public string Schedule { get; set; } = string.Empty;

        [Display(Name = "Image")]
        public IFormFile? Image { get; set; }

        [Display(Name = "Current Image URL")]
        public string? ImageUrl { get; set; }

        [Required]
        [Display(Name = "Maximum Participants")]
        [Range(1, int.MaxValue, ErrorMessage = "Maximum participants must be at least 1")]
        public int MaxParticipants { get; set; }
    }
}
