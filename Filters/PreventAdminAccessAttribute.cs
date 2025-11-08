using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UPVC.Filters
{
    /// <summary>
    /// منع الوصول للصفحات العامة إذا كان المستخدم مسجل دخول كـ Admin
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PreventAdminAccessAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var isAuthenticated = context.HttpContext.Session.GetString("AdminAuthenticated");
            
            // إذا كان Admin مسجل دخول، وجهه للـ Dashboard
            if (!string.IsNullOrEmpty(isAuthenticated) && isAuthenticated == "true")
            {
                context.Result = new RedirectToActionResult("Index", "Dashboard", new { });
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // لا حاجة لتنفيذ شيء بعد الـ Action
        }
    }
}
