using System;
using System.ComponentModel.DataAnnotations;

namespace SportSync.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Discipline { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Session { get; set; } = string.Empty;

        [Required]
        [Range(0, 10000)]
        public decimal Price { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public DateTime DateAdded { get; set; }
    }
}
