namespace UPVC.Models
{
    public class SocialMedia : BaseEntity
    {
        public SocialMediaPlatform Platform { get; set; } // Facebook, Twitter, Instagram, LinkedIn, WhatsApp, etc.
        public string Url { get; set; } = string.Empty;
        public string? IconClass { get; set; } // CSS class for icon (e.g., "bi bi-facebook")
        public int DisplayOrder { get; set; } = 0;
    }

    public enum SocialMediaPlatform
    {
        Facebook = 1,
        Twitter = 2,
        Instagram = 3,
        LinkedIn = 4,
        WhatsApp = 5,
        YouTube = 6,
        TikTok = 7,
        Snapchat = 8,
        Pinterest = 9,
        Other = 10
    }
}
