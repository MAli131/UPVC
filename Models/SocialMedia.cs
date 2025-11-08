namespace UPVC.Models
{
    public class SocialMedia : BaseEntity
    {
        public string Platform { get; set; } = string.Empty; // Facebook, Twitter, Instagram, LinkedIn, WhatsApp, etc.
        public string Url { get; set; } = string.Empty;
        public string? IconClass { get; set; } // CSS class for icon (e.g., "bi bi-facebook")
        public int DisplayOrder { get; set; } = 0;
    }
}
