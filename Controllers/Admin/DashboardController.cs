using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UPVC.Data;
using UPVC.Filters;

namespace UPVC.Controllers.Admin
{
    [Route("Admin/[controller]")]
    [AdminAuth]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.AdminUsername = HttpContext.Session.GetString("AdminUsername");
            
            // Get statistics
            ViewBag.TotalProducts = await _context.Products.CountAsync(p => !p.IsDeleted);
            ViewBag.ActiveProducts = await _context.Products.CountAsync(p => p.IsActive && !p.IsDeleted);
            ViewBag.TotalHomePages = await _context.HomePages.CountAsync(h => !h.IsDeleted);
            
            return View("~/Views/Admin/Dashboard/Index.cshtml");
        }
    }
}
