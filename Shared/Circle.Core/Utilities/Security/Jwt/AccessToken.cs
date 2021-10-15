﻿using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Circle.Core.Utilities.Security.Jwt
{
    public class AccessToken : IAccessToken
    {
        public List<string> Claims { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
    }
}