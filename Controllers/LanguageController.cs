using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace UPVC.Controllers
{
    public class LanguageController : Controller
    {
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            if (!string.IsNullOrEmpty(culture))
            {
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions 
                    { 
                        Expires = DateTimeOffset.UtcNow.AddYears(1),
                        IsEssential = true,
                        Path = "/"
                    }
                );
            }

            return LocalRedirect(returnUrl ?? "~/");
        }
    }
}
