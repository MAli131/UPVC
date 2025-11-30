using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UPVC.Data;
using UPVC.Filters;
using UPVC.Models;

namespace UPVC.Controllers.Admin
{
    [AdminAuth]
    public class AboutPagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AboutPagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AboutPages
        [HttpGet]
        [Route("Admin/AboutPages")]
        public async Task<IActionResult> Index()
        {
            var aboutPages = await _context.AboutPages
                .Include(a => a.Sections)
                .Where(a => !a.IsDeleted)
                .OrderBy(a => a.PageKey)
                .ToListAsync();
            return View("~/Views/Admin/AboutPages/Index.cshtml", aboutPages);
        }

        // GET: Admin/AboutPages/Edit/5
        [HttpGet]
        [Route("Admin/AboutPages/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutPage = await _context.AboutPages
                .Include(a => a.Sections.OrderBy(s => s.Order))
                .FirstOrDefaultAsync(a => a.Id == id);
            
            if (aboutPage == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/AboutPages/Edit.cshtml", aboutPage);
        }

        // POST: Admin/AboutPages/Edit/5
        [HttpPost]
        [Route("Admin/AboutPages/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AboutPage aboutPage)
        {
            if (id != aboutPage.Id)
            {
                return NotFound();
            }

            // Remove validation errors for navigation properties
            ModelState.Remove("Sections");
            ModelState.Remove("AboutPage");

            if (ModelState.IsValid)
            {
                try
                {
                    var existingPage = await _context.AboutPages.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
                    if (existingPage == null)
                    {
                        return NotFound();
                    }

                    // Preserve properties from database
                    aboutPage.IsActive = existingPage.IsActive;
                    aboutPage.ImagePath = existingPage.ImagePath;
                    aboutPage.PageKey = existingPage.PageKey;
                    aboutPage.CreatedAt = existingPage.CreatedAt;
                    aboutPage.IsDeleted = existingPage.IsDeleted;
                    aboutPage.DeletedAt = existingPage.DeletedAt;
                    aboutPage.UpdatedAt = DateTime.Now;
                    
                    _context.Update(aboutPage);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "تم تحديث الصفحة بنجاح";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutPageExists(aboutPage.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View("~/Views/Admin/AboutPages/Edit.cshtml", aboutPage);
        }

        // GET: Admin/AboutPages/EditSection/5
        [HttpGet]
        [Route("Admin/AboutPages/EditSection/{id}")]
        public async Task<IActionResult> EditSection(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.AboutSections.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/AboutPages/EditSection.cshtml", section);
        }

        // POST: Admin/AboutPages/EditSection/5
        [HttpPost]
        [Route("Admin/AboutPages/EditSection/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSection(int id, AboutSection section)
        {
            if (id != section.Id)
            {
                return NotFound();
            }

            // Remove validation errors for navigation properties
            ModelState.Remove("AboutPage");

            if (ModelState.IsValid)
            {
                try
                {
                    var existingSection = await _context.AboutSections.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
                    if (existingSection == null)
                    {
                        return NotFound();
                    }

                    // Preserve properties from database
                    section.IsActive = existingSection.IsActive;
                    section.IconPath = existingSection.IconPath;
                    section.AboutPageId = existingSection.AboutPageId;
                    section.SectionType = existingSection.SectionType;
                    section.Order = existingSection.Order;
                    section.CreatedAt = existingSection.CreatedAt;
                    section.IsDeleted = existingSection.IsDeleted;
                    section.DeletedAt = existingSection.DeletedAt;
                    section.DeletedAt = existingSection.DeletedAt;
                    section.UpdatedAt = DateTime.Now;
                    
                    _context.Update(section);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "تم تحديث القسم بنجاح";
                    return RedirectToAction(nameof(Edit), new { id = section.AboutPageId });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutSectionExists(section.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View("~/Views/Admin/AboutPages/EditSection.cshtml", section);
        }

        private bool AboutPageExists(int id)
        {
            return _context.AboutPages.Any(e => e.Id == id);
        }

        private bool AboutSectionExists(int id)
        {
            return _context.AboutSections.Any(e => e.Id == id);
        }
    }
}
