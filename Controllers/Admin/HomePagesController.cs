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
                .Where(h => !h.IsDeleted)
                .OrderBy(h => h.PageKey)
                .ToListAsync();
            return View("~/Views/Admin/HomePages/Index.cshtml", homePages);
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

            var homePage = await _context.HomePages.FindAsync(id);
            if (homePage == null)
            {
                return NotFound();
            }

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

            if (ModelState.IsValid)
            {
                try
                {
                    homePage.UpdatedAt = DateTime.UtcNow;
                    _context.Update(homePage);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "تم تحديث الصفحة بنجاح";
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
