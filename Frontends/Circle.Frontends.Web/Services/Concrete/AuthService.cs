using Circle.Core.Utilities.Results;
using Circle.Core.Utilities.Security.Jwt;
using Circle.Frontends.Web.Infrastructure.Extensions;
using Circle.Frontends.Web.Services.Abstract;
using Circle.Library.Entities.ComplexTypes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Services.Concrete
{
    public class AuthService : IAuthService
    {
        HttpClient _client;
        IHttpContextAccessor _httpContextAccessor;

        public AuthService(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            _client = client;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseMessage<AccessToken>> LoginAsync(LoginModel loginModel)
        {
            string json = JsonConvert.SerializeObject(loginModel,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                var response = await _client.PostAsync("Auth/Login", content);
                
                // Get string data
                string data = await response.Content.ReadAsStringAsync();

                var accessToken = JsonConvert.DeserializeObject<ResponseMessage<AccessToken>>(data);

                if (accessToken.IsSuccess)
                {
                    await SignInAsync(accessToken.Data);
                }

                // Deserialize the data
                return accessToken;
            }
        }

        public async Task<ResponseMessage<AccessToken>> LoginByRefreshTokenAsync(string refreshToken)
        {
            AccessToken token = new AccessToken()
            {
                RefreshToken = refreshToken
            };

            string json = JsonConvert.SerializeObject(token,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                var response = await _client.PostAsync("Auth/LoginWithRefreshToken", content);
                // Get string data
                string data = await response.Content.ReadAsStringAsync();
                // Deserialize the data
                return JsonConvert.DeserializeObject<ResponseMessage<AccessToken>>(data);
            }
        }

        protected Dictionary<string, string> GetTokenInfo(string token)
        {
            var TokenInfo = new Dictionary<string, string>();

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var claims = jwtSecurityToken.Claims.ToList();

            foreach (var claim in claims)
            {
                TokenInfo.Add(claim.Type, claim.Value);
            }

            return TokenInfo;
        }

        private async Task SignInAsync(AccessToken accessToken)
        {
            var tokenInfo = GetTokenInfo(accessToken.Token);

            var claims = new List<Claim>();

            foreach (var item in tokenInfo)
            {
                claims.Add(new Claim(item.Key, item.Value));
            }

            foreach (var item in accessToken.Claims)
            {
                claims.Add(new Claim(item, item));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties();

            authProperties.StoreTokens(new List<AuthenticationToken>
                {
                    new AuthenticationToken
                    {
                        Name = "AccessToken",
                        Value = accessToken.Token
                    },
                    new AuthenticationToken
                    {
                        Name = "RefreshToken",
                        Value = accessToken.RefreshToken
                    },
                    new AuthenticationToken
                    {
                        Name = "ExpiresIn",
                        Value = accessToken.Expiration.ToString("O",CultureInfo.InvariantCulture)
                    }
                });

            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}
