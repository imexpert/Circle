using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Library.Business.Constants;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;

namespace Circle.Library.Business.Handlers.Users.Commands
{
    public class UpdateUserCommand : IRequest<IResult>
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string MobilePhones { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, IResult>
        {
            private readonly IUserRepository _userRepository;

            public UpdateUserCommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }


            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var isThereAnyUser = await _userRepository.GetAsync(u => u.Id == request.UserId);

                isThereAnyUser.Firstname = request.Firstname;
                isThereAnyUser.Lastname = request.Lastname;
                isThereAnyUser.Email = request.Email;
                isThereAnyUser.MobilePhones = request.MobilePhones;
                isThereAnyUser.Address = request.Address;
                isThereAnyUser.Notes = request.Notes;

                _userRepository.Update(isThereAnyUser);
                await _userRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}