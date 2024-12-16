using System;
using System.ComponentModel.DataAnnotations;

namespace SportSync.Models
{
    public class Document
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [MaxLength(255)]
        public string FileName { get; set; }

        [Required]
        public string FilePath { get; set; }

        [Required]
        [MaxLength(50)]
        public string DocumentType { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public DateTime UploadDate { get; set; }

        public bool IsValidated { get; set; }

        public string ValidatedBy { get; set; }

        public DateTime? ValidationDate { get; set; }

        public bool IsRejected { get; set; }

        public string RejectionReason { get; set; }

        [Required]
        [MaxLength(100)]
        public string ContentType { get; set; }

        // Navigation property
        public virtual User User { get; set; }

        public Document()
        {
            UploadDate = DateTime.UtcNow;
        }
    }
}
