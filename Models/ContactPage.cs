namespace UPVC.Models
{
    public class ContactPage : BaseEntity
    {
        public string TitleEn { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string ContentEn { get; set; } = string.Empty;
        public string ContentAr { get; set; } = string.Empty;
        public string? ImagePath { get; set; }
        public string? AddressEn { get; set; }
        public string? AddressAr { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? MapUrl { get; set; }
    }
}
