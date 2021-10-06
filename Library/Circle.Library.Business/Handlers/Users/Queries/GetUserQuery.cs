using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Dtos;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;

namespace Circle.Library.Business.Handlers.Users.Queries
{
    public class GetUserQuery : IRequest<IDataResult<UserDto>>
    {
        public Guid UserId { get; set; }

        public class GetUserQueryHandler : IRequestHandler<GetUserQuery, IDataResult<UserDto>>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public GetUserQueryHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAsync(p => p.Id == request.UserId);
                var userDto = _mapper.Map<UserDto>(user);
                return new SuccessDataResult<UserDto>(userDto);
            }
        }
    }
}