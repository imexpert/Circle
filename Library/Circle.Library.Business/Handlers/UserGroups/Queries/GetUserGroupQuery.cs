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
    public class GetUserGroupQuery : IRequest<IDataResult<UserGroup>>
    {
        public int UserId { get; set; }

        public class GetUserGroupQueryHandler : IRequestHandler<GetUserGroupQuery, IDataResult<UserGroup>>
        {
            private readonly IUserGroupRepository _userGroupRepository;

            public GetUserGroupQueryHandler(IUserGroupRepository userGroupRepository)
            {
                _userGroupRepository = userGroupRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheAspect(10)]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IDataResult<UserGroup>> Handle(GetUserGroupQuery request, CancellationToken cancellationToken)
            {
                var userGroup = await _userGroupRepository.GetAsync(p => p.UserId == request.UserId);
                return new SuccessDataResult<UserGroup>(userGroup);
            }
        }
    }
}