using System.Linq;
using System.Threading.Tasks;
using Circle.Library.Business.Constants;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Security.Jwt;
using Circle.Library.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using Circle.Library.Business.Services.Authentication.Model;

namespace Circle.Library.Business.Services.Authentication
{
    /// <summary>
    /// Provider that logs in using the Circle Api database.
    /// </summary>
    public class PersonAuthenticationProvider : AuthenticationProviderBase, IAuthenticationProvider
    {
        private readonly IUserRepository _users;

        private readonly ITokenHelper _tokenHelper;

        public PersonAuthenticationProvider(AuthenticationProviderType providerType, IUserRepository users, ITokenHelper tokenHelper)
        {
            _users = users;
            ProviderType = providerType;
            _tokenHelper = tokenHelper;
        }

        public AuthenticationProviderType ProviderType { get; }

        public override async Task<LoginUserResult> Login(LoginUserCommand command)
        {
            var user = await _users.Query()
                .Where(u => u.Email == command.Email)
                .FirstOrDefaultAsync();


            return new LoginUserResult
            {
                Message = Messages.TrueButCellPhone,

                Status = LoginUserResult.LoginStatus.PhoneNumberRequired,
                MobilePhones = new string[] { user.MobilePhones }
            };
        }

        public override async Task<DArchToken> CreateToken(VerifyOtpCommand command)
        {
            var user = await _users.GetAsync(u => u.Email == command.Email);
            //user.AuthenticationProviderType = ProviderType.ToString();

            var claims = _users.GetClaims(user.Id);
            var accessToken = _tokenHelper.CreateToken<DArchToken>(user);
            accessToken.Provider = ProviderType;
            return accessToken;
        }
    }
}