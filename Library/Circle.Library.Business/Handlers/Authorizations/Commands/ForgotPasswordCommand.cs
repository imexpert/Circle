using System;
using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Library.Business.Constants;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Utilities.Results;
using Circle.Core.Utilities.Security.Hashing;
using Circle.Core.Utilities.Toolkit;
using Circle.Library.DataAccess.Abstract;
using MediatR;

namespace Circle.Library.Business.Handlers.Authorizations.Commands
{
    public class ForgotPasswordCommand : IRequest<IResult>
    {
        public string TcKimlikNo { get; set; }
        public string Email { get; set; }

        public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, IResult>
        {
            private readonly IUserRepository _userRepository;

            public ForgotPasswordCommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            /// <summary>
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IResult> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAsync(u => u.CitizenId == Convert.ToInt64(request.TcKimlikNo));

                if (user == null)
                {
                    return new ErrorResult(Messages.WrongCitizenId);
                }

                var generatedPassword = RandomPassword.CreateRandomPassword(14);
                HashingHelper.CreatePasswordHash(generatedPassword, out var passwordSalt, out var passwordHash);

                _userRepository.Update(user);

                return new SuccessResult(Messages.SendPassword + Messages.NewPassword + generatedPassword);
            }
        }
    }
}