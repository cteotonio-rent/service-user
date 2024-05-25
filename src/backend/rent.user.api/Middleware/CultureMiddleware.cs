using System.Globalization;

namespace rent.user.api.Middleware
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;

        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var supportedCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            var cultureInfo = new CultureInfo("en");
            var culture = context.Request.Headers["Accept-Language"].ToString();
            if (string.IsNullOrWhiteSpace(culture) == false
                && supportedCultures.Any(c => c.Name.Equals(culture)))
            {
                cultureInfo = new CultureInfo(culture);
            }
           
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
            await _next(context);
        }
    }
}
