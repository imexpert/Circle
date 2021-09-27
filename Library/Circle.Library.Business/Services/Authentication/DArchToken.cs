using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Security.Jwt;

namespace Circle.Library.Business.Services.Authentication
{
    public class DArchToken : AccessToken
    {
        public string ExternalUserId { get; set; }
        public AuthenticationProviderType Provider { get; set; }
        public string OnBehalfOf { get; set; }
    }
}