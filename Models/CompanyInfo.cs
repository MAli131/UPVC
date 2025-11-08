namespace UPVC.Models
{
    public class CompanyInfo : BaseEntity
    {
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Mobile { get; set; }
        public string Email { get; set; } = string.Empty;
        public string AddressEn { get; set; } = string.Empty;
        public string AddressAr { get; set; } = string.Empty;
        public string? WorkingHoursJson { get; set; } // JSON for working hours per day
        public string? LogoPath { get; set; }
        public string? FaviconPath { get; set; }
        public string? DescriptionEn { get; set; }
        public string? DescriptionAr { get; set; }
    }
}
