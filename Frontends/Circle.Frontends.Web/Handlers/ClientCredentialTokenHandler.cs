using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Handlers
{
    public class ClientCredentialTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientCredentialTokenHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Uri absoluteUrl = GenerateUri(request);
            HttpRequestMessage customRequest = new HttpRequestMessage(request.Method, absoluteUrl);
            customRequest.Content = request.Content;

            return await base.SendAsync(customRequest, cancellationToken);
        }

        private Uri GenerateUri(HttpRequestMessage request)
        {
            string culture = "tr-TR";

            if (_httpContextAccessor.HttpContext.Request.Cookies.Any(s=> s.Value == ".AspNetCore.Culture"))
            {
                culture = _httpContextAccessor.HttpContext.Request.Cookies[".AspNetCore.Culture"].Split('=')[2];
            }

            string absoluteUrl = request.RequestUri.Scheme + "://" + request.RequestUri.Authority;
            return new Uri(
                $"{absoluteUrl}/" +
                $"{request.RequestUri.Segments[1]}" +
                $"{request.RequestUri.Segments[2]}" +
                $"{culture}/" +
                $"{request.RequestUri.Segments[3]}" +
                $"{request.RequestUri.Segments[4]}");

        }
    }
}
