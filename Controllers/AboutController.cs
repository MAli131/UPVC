using Microsoft.AspNetCore.Mvc;
using UPVC.Filters;

namespace UPVC.Controllers
{
    [PreventAdminAccess]
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}