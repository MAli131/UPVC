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
                .Include(p => p.ProductDetails!)
                    .ThenInclude(pd => pd.Specifications.OrderBy(s => s.DisplayOrder))
                        .ThenInclude(ps => ps.Specification)
                .Include(p => p.ProductDetails!)
                    .ThenInclude(pd => pd.Colors.OrderBy(c => c.DisplayOrder))
                        .ThenInclude(pc => pc.Color)
                .Include(p => p.ProductDetails!)
                    .ThenInclude(pd => pd.Certificates.OrderBy(c => c.DisplayOrder))
                        .ThenInclude(pc => pc.Certificate)
                .Include(p => p.ProductDetails!)
                    .ThenInclude(pd => pd.DesignOptions.OrderBy(d => d.DisplayOrder))
                        .ThenInclude(pdo => pdo.DesignOption)
                .Where(p => p.IsActive && !p.IsDeleted)
                .OrderBy(p => p.DisplayOrder)
                .ToListAsync();
                
            return View(products);
        }

        public IActionResult Windows()
        {
            var windowProducts = GetProductsByCategory("Windows");
            return View("Category", windowProducts);
        }

        public IActionResult Doors()
        {
            var doorProducts = GetProductsByCategory("Doors");
            return View("Category", doorProducts);
        }

        public IActionResult Facades()
        {
            var facadeProducts = GetProductsByCategory("Facades");
            return View("Category", facadeProducts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductDetails!)
                    .ThenInclude(pd => pd.Specifications.OrderBy(s => s.DisplayOrder))
                        .ThenInclude(ps => ps.Specification)
                .Include(p => p.ProductDetails!)
                    .ThenInclude(pd => pd.Colors.OrderBy(c => c.DisplayOrder))
                        .ThenInclude(pc => pc.Color)
                .Include(p => p.ProductDetails!)
                    .ThenInclude(pd => pd.Certificates.OrderBy(c => c.DisplayOrder))
                        .ThenInclude(pc => pc.Certificate)
                .Include(p => p.ProductDetails!)
                    .ThenInclude(pd => pd.DesignOptions.OrderBy(d => d.DisplayOrder))
                        .ThenInclude(pdo => pdo.DesignOption)
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

        private ProductModel? GetProductById(string id)
        {
            if (id == "ema42s")
            {
                return new ProductModel
                {
                    Id = "ema42s",
                    Name = "EMAPEN 42S Window Profile",
                    Description = "High-performance uPVC window profile for energy efficiency and durability.",
                    ImageUrl = "/images/product/ema42s.png",
                    Category = "Windows"
                };
            }
            return null;
        }
    }
}