namespace UPVC.Models
{
    public class AboutPage : BaseEntity
    {
        public string PageKey { get; set; } = string.Empty; // "Index"
        public string TitleEn { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string ContentEn { get; set; } = string.Empty;
        public string ContentAr { get; set; } = string.Empty;
        public string? ImagePath { get; set; }
        public string? SubtitleEn { get; set; }
        public string? SubtitleAr { get; set; }
        public ICollection<AboutSection> Sections { get; set; } = new List<AboutSection>();

    }
    public class AboutSection : BaseEntity
    {
        public int AboutPageId { get; set; }
        public AboutPage AboutPage { get; set; }

        public AboutSectionType SectionType { get; set; } // <-- enum بدل Key

        public string TitleEn { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string ContentEn { get; set; } = string.Empty;
        public string ContentAr { get; set; } = string.Empty;
        public string? IconPath { get; set; } 
        public int Order { get; set; }
    }
    public enum AboutSectionType
    {
        Mission = 1,
        Vision = 2
    }
}
