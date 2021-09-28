using System;
using System.Linq;
using System.Threading.Tasks;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.Business.Services.Authentication.Model;
using Circle.Library.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Circle.Library.Business.Services.Authentication
{
    public abstract class AuthenticationProviderBase : IAuthenticationProvider
    {
        protected AuthenticationProviderBase()
        {
        }

        public abstract Task<LoginUserResult> Login(LoginUserCommand command);

        public abstract Task<DArchToken> CreateToken(VerifyOtpCommand command);
    }
}