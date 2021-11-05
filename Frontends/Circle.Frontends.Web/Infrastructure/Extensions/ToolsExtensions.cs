using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net.Http;

namespace Circle.Frontends.Web.Infrastructure.Extensions
{
    public static class ToolsExtensions
    {
        public static Uri GenerateUri(HttpRequestMessage request, IHttpContextAccessor context)
        {
            string culture = "tr-TR";

            if (context.HttpContext.Request.Cookies.Any(s => s.Key == ".AspNetCore.Culture"))
            {
                culture = context.HttpContext.Request.Cookies[".AspNetCore.Culture"].Split('=')[2];
            }

            string absoluteUrl = request.RequestUri.Scheme + "://" + request.RequestUri.Authority;

            if (request.RequestUri.Segments.Length > 4)
            {
                return new Uri(
                    $"{absoluteUrl}/" +
                    $"{request.RequestUri.Segments[1]}" +
                    $"{request.RequestUri.Segments[2]}" +
                    $"{culture}/" +
                    $"{request.RequestUri.Segments[3]}" +
                    $"{request.RequestUri.Segments[4]}" +
                    $"{request.RequestUri.Query}");
            }

            return new Uri(
                    $"{absoluteUrl}/" +
                    $"{request.RequestUri.Segments[1]}" +
                    $"{culture}/" +
                    $"{request.RequestUri.Segments[2]}" +
                    $"{request.RequestUri.Segments[3]}" +
                    $"{request.RequestUri.Query}");
        }
    }
}
