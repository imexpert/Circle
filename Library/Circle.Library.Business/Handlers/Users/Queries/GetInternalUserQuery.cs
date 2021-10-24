using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using Circle.Core.Entities.Concrete;

namespace Circle.Library.Business.Handlers.Users.Queries
{
    public class GetInternalUserQuery : IRequest<User>
    {
        public string Email { get; set; }

        public class GetInternalUserQueryHandler : IRequestHandler<GetInternalUserQuery, User>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public GetInternalUserQueryHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<User> Handle(GetInternalUserQuery request, CancellationToken cancellationToken)
            {
                return await _userRepository.GetAsync(p => p.Email == request.Email);
            }
        }
    }
}