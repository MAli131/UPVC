using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UPVC.Data;
using UPVC.Models;
using UPVC.Filters;

namespace UPVC.Controllers.Admin
{
    [AdminAuth]
    [Route("Admin/ChatbotFAQ")]
    public class ChatbotFAQController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChatbotFAQController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ChatbotFAQ
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var faqs = await _context.ChatbotFAQs
                .Where(f => !f.IsDeleted)
                .OrderBy(f => f.DisplayOrder)
                .ToListAsync();

            return View("~/Views/Admin/ChatbotFAQ/Index.cshtml", faqs);
        }

        // GET: Admin/ChatbotFAQ/Create
        [Route("Create")]
        public IActionResult Create()
        {
            return View("~/Views/Admin/ChatbotFAQ/Create.cshtml");
        }

        // POST: Admin/ChatbotFAQ/Create
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChatbotFAQ faq)
        {
            if (ModelState.IsValid)
            {
                faq.CreatedAt = DateTime.UtcNow;
                _context.Add(faq);
                await _context.SaveChangesAsync();
                TempData["Success"] = "تم إضافة السؤال بنجاح";
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Admin/ChatbotFAQ/Create.cshtml", faq);
        }

        // GET: Admin/ChatbotFAQ/Edit/5
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faq = await _context.ChatbotFAQs.FindAsync(id);
            if (faq == null || faq.IsDeleted)
            {
                return NotFound();
            }
            return View("~/Views/Admin/ChatbotFAQ/Edit.cshtml", faq);
        }

        // POST: Admin/ChatbotFAQ/Edit/5
        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ChatbotFAQ faq)
        {
            if (id != faq.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    faq.UpdatedAt = DateTime.UtcNow;
                    _context.Update(faq);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "تم تحديث السؤال بنجاح";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaqExists(faq.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Admin/ChatbotFAQ/Edit.cshtml", faq);
        }

        // POST: Admin/ChatbotFAQ/Delete/5
        [HttpPost]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var faq = await _context.ChatbotFAQs.FindAsync(id);
            if (faq != null)
            {
                faq.IsDeleted = true;
                faq.DeletedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        // POST: Admin/ChatbotFAQ/ToggleActive/5
        [HttpPost]
        [Route("ToggleActive/{id}")]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var faq = await _context.ChatbotFAQs.FindAsync(id);
            if (faq != null && !faq.IsDeleted)
            {
                faq.IsActive = !faq.IsActive;
                faq.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return Json(new { success = true, isActive = faq.IsActive });
            }
            return Json(new { success = false });
        }

        private bool FaqExists(int id)
        {
            return _context.ChatbotFAQs.Any(e => e.Id == id && !e.IsDeleted);
        }
    }
}
