using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.Aspects.Autofac.Performance;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Dtos;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;

namespace Circle.Library.Business.Handlers.Users.Queries
{
    public class GetUsersQuery : IRequest<IDataResult<IEnumerable<UserDto>>>
    {
        public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IDataResult<IEnumerable<UserDto>>>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            [SecuredOperation(Priority = 1)]
            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IDataResult<IEnumerable<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
            {
                var userList = await _userRepository.GetListAsync();
                var userDtoList = userList.Select(user => _mapper.Map<UserDto>(user)).ToList();

                return new SuccessDataResult<IEnumerable<UserDto>>(userDtoList);
            }
        }
    }
}