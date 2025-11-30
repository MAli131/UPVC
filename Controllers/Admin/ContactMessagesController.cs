using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UPVC.Data;
using UPVC.Filters;
using UPVC.Models;

namespace UPVC.Controllers.Admin
{
    [AdminAuth]
    [Route("Admin/ContactMessages")]
    public class ContactMessagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactMessagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ContactMessages
        [HttpGet]
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 20)
        {
            // Get total count
            var totalMessages = await _context.ContactMessages.CountAsync();
            var totalPages = (int)Math.Ceiling(totalMessages / (double)pageSize);

            // Get paginated messages
            var messages = await _context.ContactMessages
                .OrderByDescending(m => m.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Get unread count
            ViewBag.UnreadCount = await _context.ContactMessages.CountAsync(m => !m.IsRead);
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;

            return View("~/Views/Admin/ContactMessages/Index.cshtml", messages);
        }

        // GET: Admin/ContactMessages/Details/5
        [HttpGet]
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var message = await _context.ContactMessages
                .FirstOrDefaultAsync(m => m.Id == id);

            if (message == null)
                return NotFound();

            // Mark as read
            if (!message.IsRead)
            {
                message.IsRead = true;
                await _context.SaveChangesAsync();
            }

            return View("~/Views/Admin/ContactMessages/Details.cshtml", message);
        }

        // POST: Admin/ContactMessages/MarkAsRead/5
        [HttpPost]
        [Route("MarkAsRead/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var message = await _context.ContactMessages.FindAsync(id);

            if (message == null)
                return Json(new { success = false, message = "الرسالة غير موجودة" });

            message.IsRead = true;
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "تم وضع علامة كمقروء" });
        }

        // POST: Admin/ContactMessages/MarkAllAsRead
        [HttpPost]
        [Route("MarkAllAsRead")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var unreadMessages = await _context.ContactMessages
                .Where(m => !m.IsRead)
                .ToListAsync();

            foreach (var message in unreadMessages)
            {
                message.IsRead = true;
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = $"تم وضع علامة مقروء على {unreadMessages.Count} رسالة";
            return RedirectToAction(nameof(Index));
        }

        // POST: Admin/ContactMessages/Delete/5
        [HttpPost]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var message = await _context.ContactMessages.FindAsync(id);

            if (message == null)
                return Json(new { success = false, message = "الرسالة غير موجودة" });

            _context.ContactMessages.Remove(message);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "تم حذف الرسالة" });
        }

        // POST: Admin/ContactMessages/DeleteSelected
        [HttpPost]
        [Route("DeleteSelected")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSelected([FromForm] List<int> selectedIds)
        {
            if (selectedIds == null || !selectedIds.Any())
            {
                TempData["Error"] = "لم يتم اختيار أي رسائل";
                return RedirectToAction(nameof(Index));
            }

            var messages = await _context.ContactMessages
                .Where(m => selectedIds.Contains(m.Id))
                .ToListAsync();

            _context.ContactMessages.RemoveRange(messages);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"تم حذف {messages.Count} رسالة";
            return RedirectToAction(nameof(Index));
        }

        // POST: Admin/ContactMessages/DeleteAll
        [HttpPost]
        [Route("DeleteAll")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAll(bool confirmDelete = false)
        {
            if (!confirmDelete)
            {
                TempData["Error"] = "الرجاء تأكيد الحذف";
                return RedirectToAction(nameof(Index));
            }

            var allMessages = await _context.ContactMessages.ToListAsync();
            _context.ContactMessages.RemoveRange(allMessages);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"تم حذف {allMessages.Count} رسالة";
            return RedirectToAction(nameof(Index));
        }
    }
}
