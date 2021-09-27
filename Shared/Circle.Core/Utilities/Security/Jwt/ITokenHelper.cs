using Circle.Core.Entities.Concrete;

namespace Circle.Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        TAccessToken CreateToken<TAccessToken>(User user)

          where TAccessToken : IAccessToken, new();

        string GenerateRefreshToken();
    }
}