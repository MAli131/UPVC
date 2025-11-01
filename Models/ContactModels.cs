namespace UPVC.Models
{
    public class ContactFormModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }

    public class QuoteRequestModel
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public ProjectType ProjectType { get; set; }
        public ProductType ProductType { get; set; }
        public double? Area { get; set; }
        public int? Quantity { get; set; }
        public BudgetRange? Budget { get; set; }
        public DateTime? PreferredInstallationDate { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DateTime RequestedAt { get; set; } = DateTime.Now;
    }

    public enum ProjectType
    {
        Residential,
        Commercial,
        Government,
        Villa,
        Apartment,
        Office
    }

    public enum ProductType
    {
        Windows,
        Doors,
        Facades,
        Complete
    }

    public enum BudgetRange
    {
        Under10K,
        Between10K25K,
        Between25K50K,
        Between50K100K,
        Over100K
    }
}