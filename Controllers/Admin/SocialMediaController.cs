using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UPVC.Data;
using UPVC.Filters;
using UPVC.Models;

namespace UPVC.Controllers.Admin
{
    [AdminAuth]
    public class SocialMediaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SocialMediaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Admin/SocialMedia")]
        public async Task<IActionResult> Index()
        {
            var socialMedia = await _context.SocialMedias
                .Where(s => !s.IsDeleted)
                .OrderBy(s => s.DisplayOrder)
                .ToListAsync();

            return View("~/Views/Admin/SocialMedia/Index.cshtml", socialMedia);
        }

        [HttpGet]
        [Route("Admin/SocialMedia/Create")]
        public IActionResult Create()
        {
            return View("~/Views/Admin/SocialMedia/Create.cshtml");
        }

        [HttpPost]
        [Route("Admin/SocialMedia/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SocialMedia model)
        {
            // Check if platform already exists
            var exists = await _context.SocialMedias
                .Where(s => !s.IsDeleted && s.Platform == model.Platform)
                .AnyAsync();

            if (exists)
            {
                ModelState.AddModelError("Platform", "هذه المنصة موجودة بالفعل");
                return View("~/Views/Admin/SocialMedia/Create.cshtml", model);
            }

            model.CreatedAt = DateTime.UtcNow;
            _context.SocialMedias.Add(model);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "تم إضافة حساب التواصل الاجتماعي بنجاح";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("Admin/SocialMedia/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var socialMedia = await _context.SocialMedias
                .Where(s => s.Id == id && !s.IsDeleted)
                .FirstOrDefaultAsync();

            if (socialMedia == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/SocialMedia/Edit.cshtml", socialMedia);
        }

        [HttpPost]
        [Route("Admin/SocialMedia/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SocialMedia model)
        {
            var socialMedia = await _context.SocialMedias
                .Where(s => s.Id == id && !s.IsDeleted)
                .FirstOrDefaultAsync();

            if (socialMedia == null)
            {
                return NotFound();
            }

            // Don't allow changing Platform - keep the original
            // Only update URL and other fields
            socialMedia.Url = model.Url;
            socialMedia.IconClass = model.IconClass;
            socialMedia.DisplayOrder = model.DisplayOrder;
            socialMedia.IsActive = model.IsActive;
            socialMedia.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "تم تحديث حساب التواصل الاجتماعي بنجاح";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("Admin/SocialMedia/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var socialMedia = await _context.SocialMedias
                .Where(s => s.Id == id && !s.IsDeleted)
                .FirstOrDefaultAsync();

            if (socialMedia == null)
            {
                return NotFound();
            }

            socialMedia.IsDeleted = true;
            socialMedia.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "تم حذف حساب التواصل الاجتماعي بنجاح";
            return RedirectToAction(nameof(Index));
        }
    }
}
