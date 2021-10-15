using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Infrastructure.Extensions
{
    public static class HttpClientExtensions
    {
        private static IHttpContextAccessor _context;
        public static void SetContext(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor;
        }

        public static async Task<HttpResponseMessage> CustomPostAsync(this HttpClient client,string uri, StringContent content)
        {
            string exactUri = $"{_context.HttpContext.Request.Cookies[".AspNetCore.Culture"].Split('=')[2]}/{uri}";
            return await client.PostAsync(exactUri, content);
        }
    }
}
