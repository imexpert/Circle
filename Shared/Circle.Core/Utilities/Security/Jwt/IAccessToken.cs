using System;

namespace Circle.Core.Utilities.Security.Jwt
{
    public interface IAccessToken
    {
        DateTime Expiration { get; set; }
        string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}