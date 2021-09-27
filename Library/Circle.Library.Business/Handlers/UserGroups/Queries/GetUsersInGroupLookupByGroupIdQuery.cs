using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Dtos;
using Circle.Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Circle.Library.Business.Handlers.UserGroups.Queries
{
    public class GetUsersInGroupLookupByGroupIdQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
    {
        public int GroupId { get; set; }

        public class GetUsersInGroupLookupByGroupIdQueryHandler : IRequestHandler<GetUsersInGroupLookupByGroupIdQuery,
            IDataResult<IEnumerable<SelectionItem>>>
        {
            private readonly IUserGroupRepository _userGroupRepository;

            public GetUsersInGroupLookupByGroupIdQueryHandler(IUserGroupRepository userGroupRepository)
            {
                _userGroupRepository = userGroupRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheAspect(10)]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(
                GetUsersInGroupLookupByGroupIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<SelectionItem>>(
                    await _userGroupRepository.GetUsersInGroupSelectedListByGroupId(request.GroupId));
            }
        }
    }
}