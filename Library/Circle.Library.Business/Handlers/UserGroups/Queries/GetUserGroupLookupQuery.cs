using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Dtos;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;

namespace Circle.Library.Business.Handlers.UserGroups.Queries
{
    public class GetUserGroupLookupQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
    {
        public int UserId { get; set; }

        public class
            GetUserGroupLookupQueryHandler : IRequestHandler<GetUserGroupLookupQuery,
                IDataResult<IEnumerable<SelectionItem>>>
        {
            private readonly IUserGroupRepository _userGroupRepository;

            public GetUserGroupLookupQueryHandler(IUserGroupRepository userGroupRepository)
            {
                _userGroupRepository = userGroupRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheAspect(10)]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetUserGroupLookupQuery request, CancellationToken cancellationToken)
            {
                var data = await _userGroupRepository.GetUserGroupSelectedList(request.UserId);
                return new SuccessDataResult<IEnumerable<SelectionItem>>(data);
            }
        }
    }
}