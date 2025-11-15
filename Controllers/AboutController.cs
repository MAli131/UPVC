using Microsoft.AspNetCore.Mvc;
using UPVC.Filters;
using UPVC.Data;
using Microsoft.EntityFrameworkCore;

namespace UPVC.Controllers
{
    [PreventAdminAccess]
    public class AboutController : BaseController
    {
        public AboutController(ApplicationDbContext context) 
            : base(context)
        {
        }

        public IActionResult Index()
        {
            var aboutPage = _context.AboutPages
                .Include(a => a.Sections)
                .FirstOrDefault(a => a.PageKey == "About" && a.IsActive);
            
            return View(aboutPage);
        }
    }
}