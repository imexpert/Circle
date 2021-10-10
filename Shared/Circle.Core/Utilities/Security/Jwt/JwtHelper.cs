using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Circle.Core.Entities.Concrete;
using Circle.Core.Extensions;
using Circle.Core.Utilities.Security.Encyption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace Circle.Core.Utilities.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        private readonly TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;

        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public IConfiguration Configuration { get; }

        public string DecodeToken(string input)
        {
            var handler = new JwtSecurityTokenHandler();
            if (input.StartsWith("Bearer "))
            {
                input = input.Substring("Bearer ".Length);
            }

            return handler.ReadJwtToken(input).ToString();
        }

        public TAccessToken CreateToken<TAccessToken>(User user)
            where TAccessToken : IAccessToken, new()
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new TAccessToken()
            {
                Token = token,
                Expiration = _accessTokenExpiration,
                RefreshToken = GenerateRefreshToken()
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(
            TokenOptions tokenOptions,
            User user,
            SigningCredentials signingCredentials)
        {
            var jwt = new JwtSecurityToken(
                tokenOptions.Issuer,
                tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user),
                signingCredentials: signingCredentials);
            return jwt;
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
        private IEnumerable<Claim> SetClaims(User user)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Email);

            Claim claimId = new Claim("UserId", user.Id.ToString());
            claims.Add(claimId);

            if (!string.IsNullOrEmpty(user.Firstname))
            {
                claims.AddName($"{user.Firstname}");
            }

            if (!string.IsNullOrEmpty(user.Lastname))
            {
                claims.AddName($"{user.Lastname}");
            }

            //claims.Add(new Claim(ClaimTypes.Role, user.AuthenticationProviderType));


            return claims;
        }
    }
}