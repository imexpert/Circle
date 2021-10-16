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
using Circle.Core.Entities.Concrete;
using Circle.Library.Business.Helpers;

namespace Circle.Library.Business.Handlers.Users.Queries
{
    public class GetUsersQuery : IRequest<ResponseMessage<List<User>>>
    {
        public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ResponseMessage<List<User>>>
        {
            private readonly IUserRepository _userRepository;
            private readonly IReturnUtility _returnUtility;

            public GetUsersQueryHandler(
                IUserRepository userRepository, 
                IReturnUtility returnUtility)
            {
                _userRepository = userRepository;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<List<User>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
            {
                var userList = await _userRepository.GetListAsync();
                return _returnUtility.SuccessDataTable(userList, userList.Count, userList.Count);
            }
        }
    }
}