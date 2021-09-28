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
using System;

namespace Circle.Library.Business.Handlers.UserGroups.Queries
{
    public class GetUserGroupLookupByUserIdQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
    {
        public Guid UserId { get; set; }

        public class GetUserGroupLookupByUserIdQueryHandler : IRequestHandler<GetUserGroupLookupByUserIdQuery,
            IDataResult<IEnumerable<SelectionItem>>>
        {
            private readonly IUserGroupRepository _groupClaimRepository;
            private readonly IMediator _mediator;

            public GetUserGroupLookupByUserIdQueryHandler(IUserGroupRepository groupClaimRepository, IMediator mediator)
            {
                _groupClaimRepository = groupClaimRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [CacheAspect(10)]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetUserGroupLookupByUserIdQuery request, CancellationToken cancellationToken)
            {
                var data = await _groupClaimRepository.GetUserGroupSelectedList(request.UserId);
                return new SuccessDataResult<IEnumerable<SelectionItem>>(data);
            }
        }
    }
}