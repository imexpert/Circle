using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;

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
        public string Email { get; set; }

        public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, IResult>
        {
            private readonly IUserRepository _userRepository;

            public ForgotPasswordCommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<IResult> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAsync(u => u.Email == request.Email);

                if (user == null)
                {
                    return new ErrorResult("");
                }

                var generatedPassword = RandomPassword.CreateRandomPassword(14);
                string hashPassword = HashingHelper.CreatePasswordHash(generatedPassword);

                user.Password = hashPassword;
                _userRepository.Update(user);

                return new SuccessResult("" + "" + generatedPassword);
            }
        }
    }
}