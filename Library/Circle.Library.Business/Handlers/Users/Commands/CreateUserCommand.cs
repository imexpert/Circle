using System;
using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;


namespace Circle.Library.Business.Handlers.Users.Commands
{
    public class CreateUserCommand : IRequest<IResult>
    {
        public int UserId { get; set; }
        public Guid DepartmentId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string MobilePhones { get; set; }
        public bool Status { get; set; }
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
        public DateTime RecordDate { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public DateTime UpdateContactDate { get; set; }
        public string Password { get; set; }


        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IResult>
        {
            private readonly IUserRepository _userRepository;

            public CreateUserCommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var isThereAnyUser = await _userRepository.GetAsync(u => u.Email == request.Email);

                if (isThereAnyUser != null)
                {
                    return new ErrorResult(null);
                }

                var user = new User
                {
                    Email = request.Email,
                    Firstname = request.Firstname,
                    Lastname = request.Lastname,
                    Status = true,
                    Address = request.Address,
                    BirthDate = request.BirthDate,
                    DepartmentId = request.DepartmentId,
                    Gender = request.Gender,
                    Notes = request.Notes,
                    MobilePhones = request.MobilePhones
                };

                _userRepository.Add(user);
                await _userRepository.SaveChangesAsync();
                return new SuccessResult(null);
            }
        }
    }
}