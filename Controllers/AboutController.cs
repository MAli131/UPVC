using Microsoft.AspNetCore.Mvc;
using UPVC.Filters;
using UPVC.Data;

namespace UPVC.Controllers
{
    [PreventAdminAccess]
    public class AboutController : BaseController
    {
        public AboutController(ApplicationDbContext context) 
            : base(context)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}