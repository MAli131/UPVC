using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UPVC.Data;
using UPVC.ViewModels;

namespace UPVC.Controllers.Admin
{
    [Route("Admin/[controller]")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login(string? returnUrl = null)
        {
            // If already logged in, redirect to dashboard
            if (HttpContext.Session.GetString("AdminAuthenticated") == "true")
            {
                return RedirectToAction("Index", "Dashboard");
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View("~/Views/Admin/Account/Login.cshtml");
        }

        [HttpPost]
        [Route("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Account/Login.cshtml", model);
            }

            var admin = await _context.AdminUsers
                .FirstOrDefaultAsync(u => u.Username == model.Username && u.IsActive);

            if (admin == null)
            {
                ModelState.AddModelError("", "اسم المستخدم أو كلمة المرور غير صحيحة");
                return View("~/Views/Admin/Account/Login.cshtml", model);
            }

            // Verify password using BCrypt
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(model.Password, admin.PasswordHash);

            if (!isValidPassword)
            {
                ModelState.AddModelError("", "اسم المستخدم أو كلمة المرور غير صحيحة");
                return View("~/Views/Admin/Account/Login.cshtml", model);
            }

            // Update last login
            admin.LastLoginAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            // Set session
            HttpContext.Session.SetString("AdminAuthenticated", "true");
            HttpContext.Session.SetString("AdminUsername", admin.Username);
            HttpContext.Session.SetInt32("AdminId", admin.Id);

            // Redirect
            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        [Route("Logout")]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["LogoutMessage"] = "تم تسجيل الخروج بنجاح. يمكنك الآن الوصول للموقع العام.";
            return RedirectToAction("Login");
        }

        [HttpPost]
        [Route("ChangePassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            // Check if user is authenticated
            var adminId = HttpContext.Session.GetInt32("AdminId");
            if (adminId == null)
            {
                return RedirectToAction("Login");
            }

            // Validate inputs
            if (string.IsNullOrWhiteSpace(currentPassword) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                TempData["Error"] = "جميع الحقول مطلوبة";
                return Redirect(Request.Headers["Referer"].ToString());
            }

            if (newPassword != confirmPassword)
            {
                TempData["Error"] = "كلمة السر الجديدة وتأكيد كلمة السر غير متطابقتين";
                return Redirect(Request.Headers["Referer"].ToString());
            }

            if (newPassword.Length < 6)
            {
                TempData["Error"] = "كلمة السر الجديدة يجب أن تكون 6 أحرف على الأقل";
                return Redirect(Request.Headers["Referer"].ToString());
            }

            // Get admin user
            var admin = await _context.AdminUsers.FindAsync(adminId.Value);
            if (admin == null)
            {
                return RedirectToAction("Login");
            }

            // Verify current password
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(currentPassword, admin.PasswordHash);
            if (!isValidPassword)
            {
                TempData["Error"] = "كلمة السر الحالية غير صحيحة";
                return Redirect(Request.Headers["Referer"].ToString());
            }

            // Hash new password and update
            admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            admin.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            TempData["Success"] = "تم تغيير كلمة السر بنجاح";
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
