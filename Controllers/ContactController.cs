using Microsoft.AspNetCore.Mvc;
using UPVC.Models;
using UPVC.Filters;
using UPVC.Data;
using UPVC.Services;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace UPVC.Controllers
{
    [PreventAdminAccess]
    public class ContactController : BaseController
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<ContactController> _logger;

        public ContactController(
            ApplicationDbContext context,
            IEmailService emailService,
            ILogger<ContactController> logger)
            : base(context)
        {
            _emailService = emailService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var viewModel = new ViewModels.ContactViewModel
            {
                ContactPage = _context.ContactPages.FirstOrDefault(cp => cp.IsActive),
                CompanyInfo = _context.CompanyInfos.FirstOrDefault(ci => ci.IsActive),
                Categories = _context.Categories.Where(c => c.IsActive).ToList(),
                SocialMedias = _context.SocialMedias.Where(sm => sm.IsActive).OrderBy(sm => sm.DisplayOrder).ToList(),
                NewMessage = new ContactMessage()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ViewModels.ContactViewModel viewModel, int? CategoryId, string Category)
        {
            var model = viewModel.NewMessage;
            var isArabic = CultureInfo.CurrentCulture.Name.StartsWith("ar");
            
            // تعيين القيم من البارامترات المنفصلة
            model.CategoryId = CategoryId;
            model.Category = Category;
            
            // Validate Category and CategoryId
            if (string.IsNullOrWhiteSpace(Category) || !CategoryId.HasValue || CategoryId.Value == 0)
            {
                var errorMessage = isArabic ? "من فضلك اختر الفئة" : "Please select a category";
                ModelState.AddModelError("Category", errorMessage);
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    // Save to database
                    model.SubmittedAt = DateTime.Now;
                    _context.ContactMessages.Add(model);
                    await _context.SaveChangesAsync();

                    // Send email notification
                    var emailSent = await _emailService.SendContactFormEmailAsync(
                        model.Name,
                        model.Email,
                        model.Content,
                        model.Category,
                        model.Country,
                        model.City,
                        model.Telephone
                    );

                    // Update email sent status
                    if (emailSent)
                    {
                        model.EmailSent = true;
                        await _context.SaveChangesAsync();
                    }

                    var successMessage = isArabic 
                        ? "شكراً لتواصلك معنا! تم استلام رسالتك وسنتواصل معك قريباً."
                        : "Thank you for contacting us! Your message has been received and we will get back to you soon.";
                    TempData["SuccessMessage"] = successMessage;
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing contact form submission");
                    var errorMessage = isArabic
                        ? "حدث خطأ أثناء معالجة طلبك. يرجى المحاولة مرة أخرى لاحقاً."
                        : "An error occurred while processing your request. Please try again later.";
                    TempData["ErrorMessage"] = errorMessage;
                }
            }

            // Repopulate view model for redisplay
            viewModel.ContactPage = _context.ContactPages.FirstOrDefault(cp => cp.IsActive);
            viewModel.CompanyInfo = _context.CompanyInfos.FirstOrDefault(ci => ci.IsActive);
            viewModel.Categories = _context.Categories.Where(c => c.IsActive).ToList();
            viewModel.SocialMedias = _context.SocialMedias.Where(sm => sm.IsActive).OrderBy(sm => sm.DisplayOrder).ToList();
            return View(viewModel);
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