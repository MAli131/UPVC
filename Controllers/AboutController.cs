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

        public async Task<IActionResult> Index()
        {
            var aboutPage = await _context.AboutPages
                .AsNoTracking()
                .Include(a => a.Sections.Where(s => s.IsActive).OrderBy(s => s.Order))
                .FirstOrDefaultAsync(a => a.PageKey == "About" && a.IsActive);
            
            return View(aboutPage);
        }
    }
}