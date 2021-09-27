using System;
using System.Threading.Tasks;
using Circle.Core.Utilities.Results;
using Circle.Library.Business.Services.Authentication.Model;

namespace Circle.Library.Business.Services.Authentication
{
    public class AgentAuthenticationProvider : IAuthenticationProvider
    {
        public Task<LoginUserResult> Login(LoginUserCommand command)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IDataResult<DArchToken>> Verify(VerifyOtpCommand command)
        {
            throw new NotImplementedException();
        }
    }
}