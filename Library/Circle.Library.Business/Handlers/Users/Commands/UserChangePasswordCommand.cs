using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;

using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Utilities.Results;
using Circle.Core.Utilities.Security.Hashing;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;

namespace Circle.Library.Business.Handlers.Users.Commands
{
    public class UserChangePasswordCommand : IRequest<IResult>
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }

        public class UserChangePasswordCommandHandler : IRequestHandler<UserChangePasswordCommand, IResult>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public UserChangePasswordCommandHandler(IUserRepository userRepository, IMediator mediator)
            {
                _userRepository = userRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UserChangePasswordCommand request, CancellationToken cancellationToken)
            {
                var isThereAnyUser = await _userRepository.GetAsync(u => u.Id == request.UserId);
                if (isThereAnyUser == null)
                {
                    return new ErrorResult(null);
                }

                isThereAnyUser.Password = HashingHelper.CreatePasswordHash(request.Password);

                _userRepository.Update(isThereAnyUser);
                await _userRepository.SaveChangesAsync();
                return new SuccessResult(null);
            }
        }
    }
}