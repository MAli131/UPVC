namespace UPVC.Models
{
    public class ProductModel
    {
    public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Price { get; set; }
        
        public string ImageUrl { get; set; } = string.Empty;
        
        public List<string> Features { get; set; } = new List<string>();
        
        public bool IsAvailable { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        // خصائص إضافية للمنتجات
        public string Material { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Dimensions { get; set; } = string.Empty;
        public string WarrantyPeriod { get; set; } = string.Empty;
        public bool IsNew { get; set; }
        public bool IsFeatured { get; set; }
        
        // Gallery للصور الإضافية
        public List<string> ImageGallery { get; set; } = new List<string>();
        
        // التقييمات
        public double Rating { get; set; }
        public int ReviewsCount { get; set; }
    }
    
    public enum ProductCategory
    {
        Windows,    // النوافذ
        Doors,      // الأبواب
        Facades,    // الواجهات
        Accessories // الإكسسوارات
    }
}