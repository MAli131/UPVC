namespace UPVC.Models
{
    // Junction table for Many-to-Many relationship between ProductDetails and Certificate
    public class ProductCertificate : BaseEntity
    {
        public int ProductDetailsId { get; set; }
        public ProductDetails ProductDetails { get; set; } = null!;

        public int CertificateId { get; set; }
        public Certificate Certificate { get; set; } = null!;

        public int DisplayOrder { get; set; }
    }
}
