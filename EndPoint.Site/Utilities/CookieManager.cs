using System;
using Microsoft.AspNetCore.Http;

namespace EndPoint.Site.Utilities
{
    public class CookieManager
    {
        public void Add(HttpContext context, string token, string value)
        {
            context.Response.Cookies.Append(token, value, getCookieOptions(context));
        }

        public bool Contains(HttpContext context, string token)
        {
            return context.Request.Cookies.ContainsKey(token);
        }

        public string GetValue(HttpContext context, string token)
        {
            if (!context.Request.Cookies.TryGetValue(token, out var cookieValue))
                return null;
            return cookieValue;
        }

        public void Remove(HttpContext context, string token)
        {
            if (context.Request.Cookies.ContainsKey(token)) context.Response.Cookies.Delete(token);
        }

        private CookieOptions getCookieOptions(HttpContext context)
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Path = context.Request.PathBase.HasValue ? context.Request.PathBase.Value : "/",
                Secure = context.Request.IsHttps,
                Expires = DateTime.Now.AddDays(100)
            };
        }

        public Guid GetBrowserId(HttpContext context)
        {
            var browserId = GetValue(context, "BrowserId");
            if (browserId == null)
            {
                var value = Guid.NewGuid().ToString();
                Add(context, "BrowserId", value);
                browserId = value;
            }

            Guid.TryParse(browserId, out var guidBrowser);
            return guidBrowser;
        }
    }
}