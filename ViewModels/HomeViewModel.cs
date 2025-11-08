using UPVC.Models;

namespace UPVC.ViewModels
{
    public class HomeViewModel
    {
        public HomePage? HomePage { get; set; }
        public CompanyInfo? CompanyInfo { get; set; }
        public List<SocialMedia> SocialMedias { get; set; } = new List<SocialMedia>();
    }
}
