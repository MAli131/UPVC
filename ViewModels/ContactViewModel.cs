using System.Collections.Generic;
using UPVC.Models;

namespace UPVC.ViewModels
{
    public class ContactViewModel
    {
        public ContactPage? ContactPage { get; set; }
        public CompanyInfo? CompanyInfo { get; set; }
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public IEnumerable<SocialMedia> SocialMedias { get; set; } = new List<SocialMedia>();
        public ContactMessage NewMessage { get; set; } = new ContactMessage();
    }
}
