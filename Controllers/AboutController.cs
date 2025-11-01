using Microsoft.AspNetCore.Mvc;

namespace UPVC.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}