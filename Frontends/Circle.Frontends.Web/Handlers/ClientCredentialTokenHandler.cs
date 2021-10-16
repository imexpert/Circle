using Circle.Frontends.Web.Infrastructure.Extensions;
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
            Uri absoluteUrl = ToolsExtensions.GenerateUri(request, _httpContextAccessor);
            HttpRequestMessage customRequest = new HttpRequestMessage(request.Method, absoluteUrl);
            customRequest.Content = request.Content;

            return await base.SendAsync(customRequest, cancellationToken);
        }
    }
}
