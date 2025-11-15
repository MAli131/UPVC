using System.ComponentModel.DataAnnotations;

namespace UPVC.Models
{
    public class ContactMessage : BaseEntity
    {

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string? Country { get; set; }

        public string? City { get; set; }

        public string? Telephone { get; set; }

        public string Content { get; set; } = string.Empty;

        public string? Category { get; set; }

        public int? CategoryId { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.Now;

        public bool IsRead { get; set; } = false;

        public bool EmailSent { get; set; } = false;
    }
}
