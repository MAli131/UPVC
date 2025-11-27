namespace UPVC.Models
{
    public class Color : BaseEntity
    {
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string CssClass { get; set; } = string.Empty;
        public string HexCode { get; set; } = string.Empty; // For future use

        // Navigation property
        public ICollection<ProductColor> ProductColors { get; set; } = new List<ProductColor>();
    }
}
