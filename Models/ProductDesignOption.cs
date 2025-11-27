namespace UPVC.Models
{
    public class ProductDesignOption : BaseEntity
    {
        public int ProductDetailsId { get; set; }
        public int DesignOptionId { get; set; }
        public int DisplayOrder { get; set; }

        // Navigation properties
        public ProductDetails? ProductDetails { get; set; }
        public DesignOption? DesignOption { get; set; }
    }
}
