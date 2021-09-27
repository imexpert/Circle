using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.Services.Authentication;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.Aspects.Autofac.Validation;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Utilities.Results;
using MediatR;
using Circle.Library.Business.Services.Authentication.Model;
using Circle.Library.Business.Handlers.Authorizations.ValidationRules;

namespace Circle.Library.Business.Handlers.Authorizations.Queries
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, IDataResult<LoginUserResult>>
    {
        private readonly IAuthenticationCoordinator _coordinator;

        public LoginUserHandler(IAuthenticationCoordinator coordinator)
        {
            _coordinator = coordinator;
        }

        /// <summary>
        /// Allows a user to login to the system, back to the browser returns a token stored in local storage.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ValidationAspect(typeof(LoginUserValidator), Priority = 1)]
        [LogAspect(typeof(MsSqlLogger))]
        public async Task<IDataResult<LoginUserResult>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var provider = _coordinator.SelectProvider(request.Provider);
            return new SuccessDataResult<LoginUserResult>(await provider.Login(request));
        }
    }
}