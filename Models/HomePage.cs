namespace UPVC.Models
{
    // Model for managing different home page variations (Index, Home2, Home3, Home4)
    public class HomePage : BaseEntity
    {
        public string PageKey { get; set; } = string.Empty; // "Index", "Home2", "Home3", "Home4"
        public string TitleEn { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string SubtitleEn { get; set; } = string.Empty;
        public string SubtitleAr { get; set; } = string.Empty;
        public string ContentEn { get; set; } = string.Empty; // Main content/description
        public string ContentAr { get; set; } = string.Empty;
        public string? ImagePath { get; set; }
        public string? SecondaryImagePath { get; set; }
        public string? MetaDataJson { get; set; } // For additional flexible fields (stored as JSON)
    }
}
