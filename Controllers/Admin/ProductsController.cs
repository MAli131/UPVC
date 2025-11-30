using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UPVC.Data;
using UPVC.Filters;
using UPVC.Models;

namespace UPVC.Controllers.Admin
{
    [AdminAuth]
    [Route("Admin/Products")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Products
        [HttpGet]
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.ProductDetails)
                    .ThenInclude(pd => pd!.Specifications)
                .Include(p => p.ProductDetails)
                    .ThenInclude(pd => pd!.Colors)
                .Include(p => p.ProductDetails)
                    .ThenInclude(pd => pd!.DesignOptions)
                .Where(p => !p.IsDeleted)
                .OrderBy(p => p.DisplayOrder)
                .ThenBy(p => p.NameEn)
                .ToListAsync();

            return View("~/Views/Admin/Products/Index.cshtml", products);
        }

        // GET: Admin/Products/Edit/5
        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductDetails)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (product == null)
                return NotFound();

            return View("~/Views/Admin/Products/Edit.cshtml", product);
        }

        // POST: Admin/Products/Edit/5
        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
                return NotFound();

            ModelState.Remove("ProductDetails");

            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Products/Edit.cshtml", product);
            }

            var existingProduct = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (existingProduct == null)
                return NotFound();

            // Preserve fields that shouldn't be modified
            product.ImagePath = existingProduct.ImagePath;
            product.ThumbnailPath = existingProduct.ThumbnailPath;
            product.BrochurePath = existingProduct.BrochurePath;
            product.GalleryImagesJson = existingProduct.GalleryImagesJson;
            product.CreatedAt = existingProduct.CreatedAt;
            product.IsDeleted = existingProduct.IsDeleted;
            product.DeletedAt = existingProduct.DeletedAt;
            product.UpdatedAt = DateTime.Now;

            _context.Update(product);
            await _context.SaveChangesAsync();

            TempData["Success"] = "تم تحديث المنتج بنجاح";
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Products/EditSpecifications/5
        [HttpGet]
        [Route("EditSpecifications/{id}")]
        public async Task<IActionResult> EditSpecifications(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductDetails)
                    .ThenInclude(pd => pd!.Specifications)
                        .ThenInclude(ps => ps.Specification)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (product == null || product.ProductDetails == null)
                return NotFound();

            // Get all available specifications
            ViewBag.AllSpecifications = await _context.Specifications
                .Where(s => !s.IsDeleted && s.IsActive)
                .OrderBy(s => s.NameEn)
                .ToListAsync();

            return View("~/Views/Admin/Products/EditSpecifications.cshtml", product);
        }

        // POST: Admin/Products/ToggleSpecification
        [HttpPost]
        [Route("ToggleSpecification")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleSpecification(int productDetailsId, int specificationId, bool isActive)
        {
            var productSpec = await _context.ProductSpecifications
                .FirstOrDefaultAsync(ps => ps.ProductDetailsId == productDetailsId && ps.SpecificationId == specificationId);

            if (productSpec == null)
                return Json(new { success = false, message = "المواصفة غير موجودة" });

            productSpec.IsActive = isActive;
            productSpec.UpdatedAt = DateTime.Now;
            
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "تم تحديث حالة المواصفة بنجاح" });
        }

        // POST: Admin/Products/UpdateSpecification
        [HttpPost]
        [Route("UpdateSpecification")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSpecification(int specificationId, string nameAr, string nameEn, bool isActive)
        {
            var specification = await _context.Specifications
                .FirstOrDefaultAsync(s => s.Id == specificationId);

            if (specification == null)
                return Json(new { success = false, message = "المواصفة غير موجودة" });

            // Validate input
            if (string.IsNullOrWhiteSpace(nameAr) || string.IsNullOrWhiteSpace(nameEn))
                return Json(new { success = false, message = "الاسم العربي والإنجليزي مطلوبان" });

            specification.NameAr = nameAr.Trim();
            specification.NameEn = nameEn.Trim();
            specification.IsActive = isActive;
            specification.UpdatedAt = DateTime.Now;
            
            await _context.SaveChangesAsync();

            return Json(new { 
                success = true, 
                message = "تم تحديث المواصفة بنجاح",
                nameAr = specification.NameAr,
                nameEn = specification.NameEn
            });
        }

        // GET: Admin/Products/EditColors/5
        [HttpGet]
        [Route("EditColors/{id}")]
        public async Task<IActionResult> EditColors(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductDetails)
                    .ThenInclude(pd => pd!.Colors)
                        .ThenInclude(pc => pc.Color)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (product == null || product.ProductDetails == null)
                return NotFound();

            return View("~/Views/Admin/Products/EditColors.cshtml", product);
        }

        // POST: Admin/Products/ToggleColor
        [HttpPost]
        [Route("ToggleColor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleColor(int productDetailsId, int colorId, bool isActive)
        {
            var productColor = await _context.ProductColors
                .FirstOrDefaultAsync(pc => pc.ProductDetailsId == productDetailsId && pc.ColorId == colorId);

            if (productColor == null)
                return Json(new { success = false, message = "اللون غير موجود" });

            productColor.IsActive = isActive;
            productColor.UpdatedAt = DateTime.Now;
            
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "تم تحديث حالة اللون بنجاح" });
        }

        // GET: Admin/Products/EditDesignOptions/5
        [HttpGet]
        [Route("EditDesignOptions/{id}")]
        public async Task<IActionResult> EditDesignOptions(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductDetails)
                    .ThenInclude(pd => pd!.DesignOptions)
                        .ThenInclude(pdo => pdo.DesignOption)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (product == null || product.ProductDetails == null)
                return NotFound();

            return View("~/Views/Admin/Products/EditDesignOptions.cshtml", product);
        }

        // POST: Admin/Products/ToggleDesignOption
        [HttpPost]
        [Route("ToggleDesignOption")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleDesignOption(int productDetailsId, int designOptionId, bool isActive)
        {
            var productDesignOption = await _context.ProductDesignOptions
                .FirstOrDefaultAsync(pdo => pdo.ProductDetailsId == productDetailsId && pdo.DesignOptionId == designOptionId);

            if (productDesignOption == null)
                return Json(new { success = false, message = "الخيار التصميمي غير موجود" });

            productDesignOption.IsActive = isActive;
            productDesignOption.UpdatedAt = DateTime.Now;
            
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "تم تحديث حالة الخيار التصميمي بنجاح" });
        }
    }
}
