using System;
using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using Circle.Core.Utilities.Security.Hashing;

namespace Circle.Library.Business.Handlers.Users.Commands
{
    public class CreateInternalUserCommand : IRequest<bool>
    {
        public User User { get; set; }

        public class CreateInternalUserCommandHandler : IRequestHandler<CreateInternalUserCommand, bool>
        {
            private readonly IUserRepository _userRepository;

            public CreateInternalUserCommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<bool> Handle(CreateInternalUserCommand request, CancellationToken cancellationToken)
            {
                var isThereAnyUser = await _userRepository.GetAsync(u => u.Email == request.User.Email);

                if (isThereAnyUser != null)
                {
                    return false;
                }

                var user = new User
                {
                    Email = request.User.Email,
                    Password = HashingHelper.CreatePasswordHash(request.User.Password),
                    Firstname = request.User.Firstname,
                    Lastname = request.User.Lastname,
                    Status = true,
                    Address = request.User.Address,
                    BirthDate = request.User.BirthDate,
                    DepartmentId = request.User.DepartmentId,
                    Gender = request.User.Gender,
                    Notes = request.User.Notes,
                    MobilePhones = request.User.MobilePhones
                };

                _userRepository.Add(user);
                await _userRepository.SaveChangesAsync();
                return true;
            }
        }
    }
}