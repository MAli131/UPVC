namespace UPVC.Models
{
    public class ProductDetails : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        
        // Hero/Details page image
        public string? DetailHeroImagePath { get; set; }
        
        // Navigation properties
        public ICollection<ProductSpecification> Specifications { get; set; } = new List<ProductSpecification>();
        public ICollection<ProductColor> Colors { get; set; } = new List<ProductColor>();
        public ICollection<ProductCertificate> Certificates { get; set; } = new List<ProductCertificate>();
        public ICollection<ProductDesignOption> DesignOptions { get; set; } = new List<ProductDesignOption>();
    }
}
