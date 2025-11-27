namespace UPVC.Models
{
    // Junction table for Many-to-Many relationship between ProductDetails and Specification
    public class ProductSpecification : BaseEntity
    {
        public int ProductDetailsId { get; set; }
        public ProductDetails ProductDetails { get; set; } = null!;

        public int SpecificationId { get; set; }
        public Specification Specification { get; set; } = null!;

        public int DisplayOrder { get; set; }
    }
}
