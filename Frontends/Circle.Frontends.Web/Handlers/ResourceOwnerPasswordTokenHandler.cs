using Circle.Frontends.Web.Infrastructure.Extensions;
using Circle.Frontends.Web.Services.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Handlers
{
    public class ResourceOwnerPasswordTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthService _authService;

        public ResourceOwnerPasswordTokenHandler(IHttpContextAccessor httpContextAccessor, IAuthService authService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authService = authService;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("AccessToken");
            var refreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync("RefreshToken");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            Uri absoluteUrl = ToolsExtensions.GenerateUri(request, _httpContextAccessor);
            HttpRequestMessage customRequest = new HttpRequestMessage(request.Method, absoluteUrl);
            customRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            customRequest.Content = request.Content;

            var response = await base.SendAsync(customRequest, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var tokenResponse = await _authService.LoginByRefreshTokenAsync(refreshToken);
                if (tokenResponse.IsSuccess)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.Data.Token);
                    response = await base.SendAsync(request, cancellationToken);
                }
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _httpContextAccessor.HttpContext.Response.Redirect("/Login");
            }
            return response;
        }
    }
}
