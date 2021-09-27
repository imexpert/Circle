using System.Threading.Tasks;
using Circle.Core.Utilities.Results;
using Circle.Library.Business.Services.Authentication.Model;

namespace Circle.Library.Business.Services.Authentication
{
    public interface IAuthenticationProvider
    {
        Task<LoginUserResult> Login(LoginUserCommand command);
    }
}