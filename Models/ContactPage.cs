namespace UPVC.Models
{
    public class ContactPage : BaseEntity
    {
        public string TitleEn { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string SubtitleEn { get; set; } = string.Empty;
        public string SubtitleAr { get; set; } = string.Empty;
        public string? SubtitleHighlightEn { get; set; }
        public string? SubtitleHighlightAr { get; set; }
        public string ContentEn { get; set; } = string.Empty;
        public string ContentAr { get; set; } = string.Empty;
        public string? ContentHighlightEn { get; set; }
        public string? ContentHighlightAr { get; set; }
        public string? ContentOtherEn { get; set; }
        public string? ContentOtherAr { get; set; }
        public string? ImagePath { get; set; }
        public string? AddressEn { get; set; }
        public string? AddressAr { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? MapUrl { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }

    public class Category : BaseEntity
    {
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public virtual ICollection<ContactPage> ContactPages { get; set; } = new List<ContactPage>();
    }

}
