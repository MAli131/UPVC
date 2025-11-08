namespace UPVC.Models
{
    public class AboutPage : BaseEntity
    {
        public string TitleEn { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string ContentEn { get; set; } = string.Empty;
        public string ContentAr { get; set; } = string.Empty;
        public string? ImagePath { get; set; }
        public string? SubtitleEn { get; set; }
        public string? SubtitleAr { get; set; }
        public string? AdditionalContentJson { get; set; } // For sections, stats, etc.
    }
}
