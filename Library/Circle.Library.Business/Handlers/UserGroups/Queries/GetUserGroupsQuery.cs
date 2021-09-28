using System.Collections.Generic;
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

namespace Circle.Library.Business.Handlers.UserGroups.Queries
{
    public class GetUserGroupsQuery : IRequest<IDataResult<IEnumerable<UserGroup>>>
    {
        public class
            GetUserGroupsQueryHandler : IRequestHandler<GetUserGroupsQuery, IDataResult<IEnumerable<UserGroup>>>
        {
            private readonly IUserGroupRepository _userGroupRepository;

            public GetUserGroupsQueryHandler(IUserGroupRepository userGroupRepository)
            {
                _userGroupRepository = userGroupRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheAspect(10)]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IDataResult<IEnumerable<UserGroup>>> Handle(GetUserGroupsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<UserGroup>>(await _userGroupRepository.GetListAsync());
            }
        }
    }
}