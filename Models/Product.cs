namespace UPVC.Models
{
    public class Product : BaseEntity
    {
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string? DetailsEn { get; set; } // Long description for details page
        public string? DetailsAr { get; set; }
        public string? ImagePath { get; set; }
        public string? ThumbnailPath { get; set; }
        public string? GalleryImagesJson { get; set; } // JSON array of image paths
        public string? CategoryEn { get; set; }
        public string? CategoryAr { get; set; }
        public int DisplayOrder { get; set; } = 0;
    }
}
