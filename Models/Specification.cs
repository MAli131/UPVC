namespace UPVC.Models
{
    public class Specification : BaseEntity
    {
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;

        // Navigation property
        public ICollection<ProductSpecification> ProductSpecifications { get; set; } = new List<ProductSpecification>();
    }
}
