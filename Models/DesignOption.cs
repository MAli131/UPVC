namespace UPVC.Models
{
    public class DesignOption : BaseEntity
    {
        public required string ImagePath { get; set; }
        public string? AltText { get; set; }

        // Navigation property
        public ICollection<ProductDesignOption> ProductDesignOptions { get; set; } = new List<ProductDesignOption>();
    }
}
