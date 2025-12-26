namespace UPVC.Middleware
{
    public class ImageOptimizationMiddleware
    {
        private readonly RequestDelegate _next;

        public ImageOptimizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();
            
            // إضافة Cache Headers للصور والملفات الثابتة
            if (path != null)
            {
                if (path.EndsWith(".jpg") || 
                    path.EndsWith(".jpeg") || 
                    path.EndsWith(".png") || 
                    path.EndsWith(".svg") ||
                    path.EndsWith(".webp") ||
                    path.EndsWith(".gif") ||
                    path.EndsWith(".ico"))
                {
                    // Cache للصور لمدة سنة
                    context.Response.Headers.Append("Cache-Control", "public,max-age=31536000,immutable");
                    context.Response.Headers.Append("Expires", DateTime.UtcNow.AddYears(1).ToString("R"));
                }
                else if (path.EndsWith(".css") || 
                         path.EndsWith(".js") || 
                         path.EndsWith(".woff") || 
                         path.EndsWith(".woff2") || 
                         path.EndsWith(".ttf") || 
                         path.EndsWith(".eot"))
                {
                    // Cache للملفات الثابتة لمدة 7 أيام
                    context.Response.Headers.Append("Cache-Control", "public,max-age=604800");
                    context.Response.Headers.Append("Expires", DateTime.UtcNow.AddDays(7).ToString("R"));
                }
            }

            await _next(context);
        }
    }
}
