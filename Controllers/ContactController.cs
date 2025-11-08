using Microsoft.AspNetCore.Mvc;
using UPVC.Models;
using UPVC.Filters;

namespace UPVC.Controllers
{
    [PreventAdminAccess]
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                // معالجة إرسال النموذج
                // يمكن إرسال إيميل، حفظ في قاعدة البيانات، إلخ
                
                TempData["SuccessMessage"] = "تم إرسال رسالتك بنجاح. سنتواصل معك في أقرب وقت ممكن.";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult GetQuote()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetQuote(QuoteRequestModel model)
        {
            if (ModelState.IsValid)
            {
                // معالجة طلب العرض
                TempData["SuccessMessage"] = "تم استلام طلبك للحصول على عرض سعر. سنتواصل معك خلال 24 ساعة.";
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}