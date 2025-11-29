using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UPVC.Data;
using UPVC.Filters;
using UPVC.Models;

namespace UPVC.Controllers.Admin
{
    [AdminAuth]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Admin/Categories")]
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories
                .Where(c => !c.IsDeleted)
                .OrderBy(c => c.NameEn)
                .ToListAsync();

            return View("~/Views/Admin/Categories/Index.cshtml", categories);
        }

        [HttpGet]
        [Route("Admin/Categories/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.Categories
                .Where(c => c.Id == id && !c.IsDeleted)
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/Categories/Edit.cshtml", category);
        }

        [HttpPost]
        [Route("Admin/Categories/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Categories/Edit.cshtml", model);
            }

            var category = await _context.Categories
                .Where(c => c.Id == id && !c.IsDeleted)
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return NotFound();
            }

            category.NameEn = model.NameEn;
            category.NameAr = model.NameAr;
            category.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "تم تحديث الفئة بنجاح";
            return RedirectToAction(nameof(Index));
        }
    }
}
