using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UPVC.Data;
using UPVC.Filters;
using UPVC.Models;

namespace UPVC.Controllers.Admin
{
    [AdminAuth]
    public class ContactPageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactPageController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Admin/ContactPage/Edit")]
        public async Task<IActionResult> Edit()
        {
            // Get the first ContactPage record (assuming single record)
            var contactPage = await _context.ContactPages.FirstOrDefaultAsync();

            if (contactPage == null)
            {
                // Create a new one if doesn't exist
                contactPage = new ContactPage();
                _context.ContactPages.Add(contactPage);
                await _context.SaveChangesAsync();
            }

            return View("~/Views/Admin/ContactPage/Edit.cshtml", contactPage);
        }

        [HttpPost]
        [Route("Admin/ContactPage/Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ContactPage model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/ContactPage/Edit.cshtml", model);
            }

            var contactPage = await _context.ContactPages.FirstOrDefaultAsync();

            if (contactPage == null)
            {
                return NotFound();
            }

            // Update all fields
            contactPage.TitleEn = model.TitleEn;
            contactPage.TitleAr = model.TitleAr;
            contactPage.SubtitleEn = model.SubtitleEn;
            contactPage.SubtitleAr = model.SubtitleAr;
            contactPage.SubtitleHighlightEn = model.SubtitleHighlightEn;
            contactPage.SubtitleHighlightAr = model.SubtitleHighlightAr;
            contactPage.ContentEn = model.ContentEn;
            contactPage.ContentAr = model.ContentAr;
            contactPage.ContentHighlightEn = model.ContentHighlightEn;
            contactPage.ContentHighlightAr = model.ContentHighlightAr;
            contactPage.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "تم تحديث بيانات صفحة اتصل بنا بنجاح";
            return RedirectToAction(nameof(Edit));
        }
    }
}
