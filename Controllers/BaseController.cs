using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UPVC.Data;

namespace UPVC.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ApplicationDbContext _context;

        public BaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Load CompanyInfo for all views
            ViewBag.CompanyInfo = _context.CompanyInfos.FirstOrDefault(ci => ci.IsActive);
            base.OnActionExecuting(context);
        }
    }
}
