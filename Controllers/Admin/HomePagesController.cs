using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UPVC.Data;
using UPVC.Filters;
using UPVC.Models;

namespace UPVC.Controllers.Admin
{
    [AdminAuth]
    public class HomePagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomePagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/HomePages
        [HttpGet]
        [Route("Admin/HomePages")]
        public async Task<IActionResult> Index()
        {
            var homePages = await _context.HomePages
                .Include(h => h.Sections.Where(s => !s.IsDeleted))
                .Where(h => !h.IsDeleted)
                .OrderBy(h => h.PageKey)
                .ToListAsync();
            return View("~/Views/Admin/HomePages/Index.cshtml", homePages);
        }

        // GET: Admin/HomePages/EditIndex
        [HttpGet]
        [Route("Admin/HomePages/EditIndex")]
        public async Task<IActionResult> EditIndex()
        {
            var homePage = await _context.HomePages
                .FirstOrDefaultAsync(h => h.PageKey == "Index" && !h.IsDeleted);
            
            if (homePage == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/HomePages/EditIndex.cshtml", homePage);
        }

        // POST: Admin/HomePages/EditIndex
        [HttpPost]
        [Route("Admin/HomePages/EditIndex")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditIndex(HomePage homePage)
        {
            ModelState.Remove("Sections");
            ModelState.Remove("ImagePath");
            ModelState.Remove("SecondaryImagePath");
            ModelState.Remove("CreatedAt");
            ModelState.Remove("IsDeleted");
            ModelState.Remove("DeletedAt");

            var existingPage = await _context.HomePages
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PageKey == "Index");

            if (existingPage == null)
            {
                return NotFound();
            }

            homePage.Id = existingPage.Id;
            homePage.PageKey = "Index";
            homePage.ImagePath = existingPage.ImagePath;
            homePage.SecondaryImagePath = existingPage.SecondaryImagePath;
            homePage.IsActive = existingPage.IsActive;
            homePage.CreatedAt = existingPage.CreatedAt;
            homePage.IsDeleted = existingPage.IsDeleted;
            homePage.DeletedAt = existingPage.DeletedAt;
            homePage.UpdatedAt = DateTime.Now;
            homePage.IsActive = true;
            // Preserve ContentAr and ContentEn if they are empty
            if (string.IsNullOrWhiteSpace(homePage.ContentAr))
            {
                homePage.ContentAr = existingPage.ContentAr;
            }
            if (string.IsNullOrWhiteSpace(homePage.ContentEn))
            {
                homePage.ContentEn = existingPage.ContentEn;
            }

            _context.Update(homePage);
            await _context.SaveChangesAsync();
            
            TempData["Success"] = "تم تحديث صفحة Index بنجاح";
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/HomePages/EditHome2
        [HttpGet]
        [Route("Admin/HomePages/EditHome2")]
        public async Task<IActionResult> EditHome2()
        {
            var homePage = await _context.HomePages
                .Include(h => h.Sections.Where(s => !s.IsDeleted))
                .FirstOrDefaultAsync(h => h.PageKey == "Home2" && !h.IsDeleted);
            
            if (homePage == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/HomePages/EditHome2.cshtml", homePage);
        }

        // POST: Admin/HomePages/EditHome2
        [HttpPost]
        [Route("Admin/HomePages/EditHome2")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHome2(HomePage homePage)
        {
            ModelState.Remove("Sections");
            ModelState.Remove("ImagePath");
            ModelState.Remove("SecondaryImagePath");
            ModelState.Remove("CreatedAt");
            ModelState.Remove("IsDeleted");
            ModelState.Remove("DeletedAt");

            var existingPage = await _context.HomePages
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PageKey == "Home2");

            if (existingPage == null)
            {
                return NotFound();
            }

            homePage.Id = existingPage.Id;
            homePage.PageKey = "Home2";
            homePage.ImagePath = existingPage.ImagePath;
            homePage.SecondaryImagePath = existingPage.SecondaryImagePath;
            homePage.IsActive = existingPage.IsActive;
            homePage.CreatedAt = existingPage.CreatedAt;
            homePage.IsDeleted = existingPage.IsDeleted;
            homePage.DeletedAt = existingPage.DeletedAt;
            homePage.UpdatedAt = DateTime.Now;
            homePage.IsActive = true;
            // Preserve ContentAr and ContentEn if they are empty
            if (string.IsNullOrWhiteSpace(homePage.ContentAr))
            {
                homePage.ContentAr = existingPage.ContentAr;
            }
            if (string.IsNullOrWhiteSpace(homePage.ContentEn))
            {
                homePage.ContentEn = existingPage.ContentEn;
            }

            _context.Update(homePage);
            await _context.SaveChangesAsync();
            
            TempData["Success"] = "تم تحديث صفحة Home2 بنجاح";
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/HomePages/EditHome3
        [HttpGet]
        [Route("Admin/HomePages/EditHome3")]
        public async Task<IActionResult> EditHome3()
        {
            var homePage = await _context.HomePages
                .Include(h => h.Sections.Where(s => !s.IsDeleted))
                .FirstOrDefaultAsync(h => h.PageKey == "Home3" && !h.IsDeleted);
            
            if (homePage == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/HomePages/EditHome3.cshtml", homePage);
        }

        // POST: Admin/HomePages/EditHome3
        [HttpPost]
        [Route("Admin/HomePages/EditHome3")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHome3(HomePage homePage)
        {
            ModelState.Remove("Sections");
            ModelState.Remove("ImagePath");
            ModelState.Remove("SecondaryImagePath");
            ModelState.Remove("CreatedAt");
            ModelState.Remove("IsDeleted");
            ModelState.Remove("DeletedAt");

            var existingPage = await _context.HomePages
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PageKey == "Home3");

            if (existingPage == null)
            {
                return NotFound();
            }

            homePage.Id = existingPage.Id;
            homePage.PageKey = "Home3";
            homePage.ImagePath = existingPage.ImagePath;
            homePage.SecondaryImagePath = existingPage.SecondaryImagePath;
            homePage.IsActive = existingPage.IsActive;
            homePage.CreatedAt = existingPage.CreatedAt;
            homePage.IsDeleted = existingPage.IsDeleted;
            homePage.DeletedAt = existingPage.DeletedAt;
            homePage.UpdatedAt = DateTime.Now;
            homePage.IsActive = true;
            // Preserve ContentAr and ContentEn if they are empty
            if (string.IsNullOrWhiteSpace(homePage.ContentAr))
            {
                homePage.ContentAr = existingPage.ContentAr;
            }
            if (string.IsNullOrWhiteSpace(homePage.ContentEn))
            {
                homePage.ContentEn = existingPage.ContentEn;
            }

            _context.Update(homePage);
            await _context.SaveChangesAsync();
            
            TempData["Success"] = "تم تحديث صفحة Home3 بنجاح";
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/HomePages/EditHome4
        [HttpGet]
        [Route("Admin/HomePages/EditHome4")]
        public async Task<IActionResult> EditHome4()
        {
            var homePage = await _context.HomePages
                .FirstOrDefaultAsync(h => h.PageKey == "Home4" && !h.IsDeleted);
            
            if (homePage == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/HomePages/EditHome4.cshtml", homePage);
        }

        // POST: Admin/HomePages/EditHome4
        [HttpPost]
        [Route("Admin/HomePages/EditHome4")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHome4(HomePage homePage)
        {
            ModelState.Remove("Sections");
            ModelState.Remove("ImagePath");
            ModelState.Remove("SecondaryImagePath");
            ModelState.Remove("CreatedAt");
            ModelState.Remove("IsDeleted");
            ModelState.Remove("DeletedAt");

            var existingPage = await _context.HomePages
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PageKey == "Home4");

            if (existingPage == null)
            {
                return NotFound();
            }

            homePage.Id = existingPage.Id;
            homePage.PageKey = "Home4";
            homePage.ImagePath = existingPage.ImagePath;
            homePage.SecondaryImagePath = existingPage.SecondaryImagePath;
            homePage.IsActive = existingPage.IsActive;
            homePage.CreatedAt = existingPage.CreatedAt;
            homePage.IsDeleted = existingPage.IsDeleted;
            homePage.DeletedAt = existingPage.DeletedAt;
            homePage.UpdatedAt = DateTime.Now;
            homePage.IsActive = true;
            // Preserve ContentAr and ContentEn if they are empty
            if (string.IsNullOrWhiteSpace(homePage.ContentAr))
            {
                homePage.ContentAr = existingPage.ContentAr;
            }
            if (string.IsNullOrWhiteSpace(homePage.ContentEn))
            {
                homePage.ContentEn = existingPage.ContentEn;
            }

            _context.Update(homePage);
            await _context.SaveChangesAsync();
            
            TempData["Success"] = "تم تحديث صفحة Home4 بنجاح";
            return RedirectToAction(nameof(Index));
        }

        // POST: Admin/HomePages/EditSection
        [HttpPost]
        [Route("Admin/HomePages/EditSection")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSection(HomePageSection homePageSection)
        {
            ModelState.Remove("HomePage"); // Ignore navigation property
            ModelState.Remove("CreatedAt");
            ModelState.Remove("IsDeleted");
            ModelState.Remove("DeletedAt");

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["Error"] = "حدث خطأ في البيانات المدخلة: " + string.Join(", ", errors);
                
                var homePage = await _context.HomePages.FindAsync(homePageSection.HomePageId);
                if (homePage?.PageKey == "Home2")
                    return RedirectToAction(nameof(EditHome2), new { id = homePageSection.HomePageId });
                else if (homePage?.PageKey == "Home3")
                    return RedirectToAction(nameof(EditHome3), new { id = homePageSection.HomePageId });
                    
                return RedirectToAction(nameof(Index));
            }

            var existingSection = await _context.HomePageSections
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == homePageSection.Id);

            if (existingSection == null)
            {
                TempData["Error"] = "القسم غير موجود";
                return RedirectToAction(nameof(Index));
            }

            // Preserve fields
            homePageSection.HomePageId = existingSection.HomePageId;
            homePageSection.Order = existingSection.Order;
            homePageSection.ImagePath = existingSection.ImagePath;
            homePageSection.CreatedAt = existingSection.CreatedAt;
            homePageSection.IsDeleted = existingSection.IsDeleted;
            homePageSection.DeletedAt = existingSection.DeletedAt;
            homePageSection.UpdatedAt = DateTime.Now;
            homePageSection.IsActive = true;
            _context.Update(homePageSection);
            await _context.SaveChangesAsync();

            TempData["Success"] = "تم تحديث القسم بنجاح";
            
            // الرجوع إلى الصفحة المناسبة
            var homePageResult = await _context.HomePages.FindAsync(existingSection.HomePageId);
            if (homePageResult?.PageKey == "Home2")
            {
                return RedirectToAction(nameof(EditHome2), new { id = existingSection.HomePageId });
            }
            else if (homePageResult?.PageKey == "Home3")
            {
                return RedirectToAction(nameof(EditHome3), new { id = existingSection.HomePageId });
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/HomePages/Edit/5
        [HttpGet]
        [Route("Admin/HomePages/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homePage = await _context.HomePages
                .Include(h => h.Sections.Where(s => !s.IsDeleted))
                .FirstOrDefaultAsync(h => h.Id == id);
            
            if (homePage == null)
            {
                return NotFound();
            }

            ViewBag.Sections = homePage.Sections.ToList();
            return View("~/Views/Admin/HomePages/Edit.cshtml", homePage);
        }

        // POST: Admin/HomePages/Edit/5
        [HttpPost]
        [Route("Admin/HomePages/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HomePage homePage)
        {
            if (id != homePage.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Sections"); // Ignore navigation property

            if (ModelState.IsValid)
            {
                try
                {
                    // Get existing entity to preserve values
                    var existingPage = await _context.HomePages
                        .AsNoTracking()
                        .FirstOrDefaultAsync(p => p.Id == id);

                    if (existingPage == null)
                    {
                        return NotFound();
                    }

                    // Preserve fields that shouldn't be modified
                    homePage.PageKey = existingPage.PageKey;
                    homePage.ImagePath = existingPage.ImagePath;
                    homePage.SecondaryImagePath = existingPage.SecondaryImagePath;
                    homePage.IsActive = existingPage.IsActive; // Default to existing value
                    homePage.CreatedAt = existingPage.CreatedAt;
                    homePage.IsDeleted = existingPage.IsDeleted;
                    homePage.DeletedAt = existingPage.DeletedAt;
                    homePage.UpdatedAt = DateTime.Now;
                    homePage.IsActive = true;
                    _context.Update(homePage);
                    await _context.SaveChangesAsync();
                    
                    TempData["SuccessMessage"] = "تم تحديث الصفحة بنجاح";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomePageExists(homePage.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View("~/Views/Admin/HomePages/Edit.cshtml", homePage);
        }

        private bool HomePageExists(int id)
        {
            return _context.HomePages.Any(e => e.Id == id);
        }
    }
}
