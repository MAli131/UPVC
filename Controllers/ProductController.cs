using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UPVC.Models;
using UPVC.Filters;
using UPVC.Data;

namespace UPVC.Controllers
{
    [PreventAdminAccess]
    public class ProductController : BaseController
    {
        public ProductController(ApplicationDbContext context) 
            : base(context)
        {
        }
        
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .AsSplitQuery()
                .Include(p => p.ProductDetails!)
                    .ThenInclude(pd => pd.Specifications.Where(s => s.IsActive).OrderBy(s => s.DisplayOrder))
                        .ThenInclude(ps => ps.Specification)
                .Include(p => p.ProductDetails!)
                    .ThenInclude(pd => pd.Colors.Where(c => c.IsActive).OrderBy(c => c.DisplayOrder))
                        .ThenInclude(pc => pc.Color)
                .Include(p => p.ProductDetails!)
                    .ThenInclude(pd => pd.Certificates.Where(c => c.IsActive).OrderBy(c => c.DisplayOrder))
                        .ThenInclude(pc => pc.Certificate)
                .Include(p => p.ProductDetails!)
                    .ThenInclude(pd => pd.DesignOptions.Where(d => d.IsActive).OrderBy(d => d.DisplayOrder))
                        .ThenInclude(pdo => pdo.DesignOption)
                .Where(p => p.IsActive && !p.IsDeleted)
                .OrderBy(p => p.DisplayOrder)
                .AsNoTracking()
                .ToListAsync();
                
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .AsSplitQuery()
                .Include(p => p.ProductDetails!)
                    .ThenInclude(pd => pd.Specifications.Where(s => s.IsActive).OrderBy(s => s.DisplayOrder))
                        .ThenInclude(ps => ps.Specification)
                .Include(p => p.ProductDetails!)
                    .ThenInclude(pd => pd.Colors.Where(c => c.IsActive).OrderBy(c => c.DisplayOrder))
                        .ThenInclude(pc => pc.Color)
                .Include(p => p.ProductDetails!)
                    .ThenInclude(pd => pd.Certificates.Where(c => c.IsActive).OrderBy(c => c.DisplayOrder))
                        .ThenInclude(pc => pc.Certificate)
                .Include(p => p.ProductDetails!)
                    .ThenInclude(pd => pd.DesignOptions.Where(d => d.IsActive).OrderBy(d => d.DisplayOrder))
                        .ThenInclude(pdo => pdo.DesignOption)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive && !p.IsDeleted);
                
            if (product == null)
            {
                return NotFound();
            }
            
            return View(product);
        }

        // طرق مساعدة - يجب استبدالها بالوصول إلى قاعدة البيانات
        private List<ProductModel> GetAllProducts()
        {
            return new List<ProductModel>();
        }

        private List<ProductModel> GetProductsByCategory(string category)
        {
            return new List<ProductModel>();
        }

 
    }
}