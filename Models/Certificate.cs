namespace UPVC.Models
{
    public class Certificate : BaseEntity
    {
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string AltText { get; set; } = string.Empty;
        public int Width { get; set; } = 40;

        // Navigation property
        public ICollection<ProductCertificate> ProductCertificates { get; set; } = new List<ProductCertificate>();
    }
}
