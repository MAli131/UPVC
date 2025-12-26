using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
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
            // Load CompanyInfo for all views with AsNoTracking for better performance
            ViewBag.CompanyInfo = _context.CompanyInfos
                .AsNoTracking()
                .FirstOrDefault(ci => ci.IsActive && !ci.IsDeleted);
            
            base.OnActionExecuting(context);
        }
    }
}
