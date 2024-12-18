using System;
using System.ComponentModel.DataAnnotations;

namespace SportSync.Models.ViewModels
{
    public class SessionViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Discipline")]
        public int SportId { get; set; }

        public string SportName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Coach")]
        public int CoachId { get; set; }

        public string CoachName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        [Required]
        [Range(1, 100)]
        [Display(Name = "Maximum Capacity")]
        public int MaxCapacity { get; set; }

        public int CurrentParticipants { get; set; }
    }
}
