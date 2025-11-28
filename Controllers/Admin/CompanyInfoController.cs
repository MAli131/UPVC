using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UPVC.Data;
using UPVC.Filters;

namespace UPVC.Controllers.Admin
{
    [Route("Admin/[controller]")]
    [AdminAuth]
    public class CompanyInfoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CompanyInfoController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Edit()
        {
            var companyInfo = await _context.CompanyInfos
                .Where(c => !c.IsDeleted)
                .FirstOrDefaultAsync();

            if (companyInfo == null)
            {
                companyInfo = new Models.CompanyInfo();
            }

            return View("~/Views/Admin/CompanyInfo/Edit.cshtml", companyInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Models.CompanyInfo model, IFormFile? logoFile, IFormFile? faviconFile)
        {
            var companyInfo = await _context.CompanyInfos
                .Where(c => !c.IsDeleted)
                .FirstOrDefaultAsync();

            if (companyInfo == null)
            {
                companyInfo = new Models.CompanyInfo
                {
                    CreatedAt = DateTime.UtcNow
                };
                _context.CompanyInfos.Add(companyInfo);
            }

            companyInfo.NameEn = model.NameEn;
            companyInfo.NameAr = model.NameAr;
            companyInfo.Phone = model.Phone;
            companyInfo.Mobile = model.Mobile;
            companyInfo.Email = model.Email;
            companyInfo.AddressEn = model.AddressEn;
            companyInfo.AddressAr = model.AddressAr;
            companyInfo.DescriptionEn = model.DescriptionEn;
            companyInfo.DescriptionAr = model.DescriptionAr;
            companyInfo.SloganEn = model.SloganEn;
            companyInfo.SloganAr = model.SloganAr;
            companyInfo.CopyrightTextEn = model.CopyrightTextEn;
            companyInfo.CopyrightTextAr = model.CopyrightTextAr;
            companyInfo.WorkingHoursJson = model.WorkingHoursJson;
            companyInfo.IsActive = model.IsActive;
            companyInfo.UpdatedAt = DateTime.UtcNow;

            // Handle logo upload
            if (logoFile != null && logoFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "company");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = $"logo_{Guid.NewGuid()}{Path.GetExtension(logoFile.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await logoFile.CopyToAsync(fileStream);
                }

                // Delete old logo if exists
                if (!string.IsNullOrEmpty(companyInfo.LogoPath))
                {
                    var oldLogoPath = Path.Combine(_environment.WebRootPath, companyInfo.LogoPath.TrimStart('/'));
                    if (System.IO.File.Exists(oldLogoPath))
                    {
                        System.IO.File.Delete(oldLogoPath);
                    }
                }

                companyInfo.LogoPath = $"/uploads/company/{uniqueFileName}";
            }

            // Handle favicon upload
            if (faviconFile != null && faviconFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "company");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = $"favicon_{Guid.NewGuid()}{Path.GetExtension(faviconFile.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await faviconFile.CopyToAsync(fileStream);
                }

                // Delete old favicon if exists
                if (!string.IsNullOrEmpty(companyInfo.FaviconPath))
                {
                    var oldFaviconPath = Path.Combine(_environment.WebRootPath, companyInfo.FaviconPath.TrimStart('/'));
                    if (System.IO.File.Exists(oldFaviconPath))
                    {
                        System.IO.File.Delete(oldFaviconPath);
                    }
                }

                companyInfo.FaviconPath = $"/uploads/company/{uniqueFileName}";
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "تم حفظ بيانات الشركة بنجاح";
            return RedirectToAction(nameof(Edit));
        }
    }
}
