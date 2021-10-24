using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Core.Utilities.Security.Hashing;
using Circle.Library.DataAccess.Abstract;
using MediatR;

namespace Circle.Library.Business.Handlers.Authorizations.Commands
{
    public class RegisterUserCommand : IRequest<IResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }


        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IResult>
        {
            private readonly IUserRepository _userRepository;


            public RegisterUserCommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            [SecuredOperation]
            public async Task<IResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                var isThereAnyUser = await _userRepository.GetAsync(u => u.Email == request.Email);

                if (isThereAnyUser != null)
                {
                    return new ErrorResult(null);
                }

                string hashPassword = HashingHelper.CreatePasswordHash(request.Password);
                var user = new User
                {
                    Email = request.Email,
                    Password = hashPassword,
                    Firstname = request.Firstname,
                    Lastname = request.Lastname,
                    Status = true
                };

                _userRepository.Add(user);
                await _userRepository.SaveChangesAsync();
                return new SuccessResult(null);
            }
        }
    }
}