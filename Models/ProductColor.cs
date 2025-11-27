namespace UPVC.Models
{
    // Junction table for Many-to-Many relationship between ProductDetails and Color
    public class ProductColor : BaseEntity
    {
        public int ProductDetailsId { get; set; }
        public ProductDetails ProductDetails { get; set; } = null!;

        public int ColorId { get; set; }
        public Color Color { get; set; } = null!;

        public int DisplayOrder { get; set; }
    }
}
