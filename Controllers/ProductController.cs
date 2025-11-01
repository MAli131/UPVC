using Microsoft.AspNetCore.Mvc;
using UPVC.Models;

namespace UPVC.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            // يمكن جلب المنتجات من قاعدة البيانات هنا
            var products = GetAllProducts();
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

        public IActionResult Details(string id)
        {
            var product = GetProductById(id);
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

        private ProductModel GetProductById(string id)
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