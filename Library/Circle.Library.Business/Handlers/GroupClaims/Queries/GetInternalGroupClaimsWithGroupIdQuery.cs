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
using Circle.Library.Business.Helpers;
using System.Linq;
using Circle.Core.Utilities.Messages;
using System;

namespace Circle.Library.Business.Handlers.GroupClaims.Queries
{
    public class GetInternalGroupClaimsWithGroupIdQuery : IRequest<List<GroupClaim>>
    {
        public Guid GroupId { get; set; }
        public class GetInternalGroupClaimsWithGroupIdQueryHandler : IRequestHandler<GetInternalGroupClaimsWithGroupIdQuery, List<GroupClaim>>
        {
            private readonly IGroupClaimRepository _groupClaimRepository;
            private readonly IReturnUtility _returnUtility;

            public GetInternalGroupClaimsWithGroupIdQueryHandler(IGroupClaimRepository groupClaimRepository, IReturnUtility returnUtility)
            {
                _groupClaimRepository = groupClaimRepository;
                _returnUtility = returnUtility;
            }

            public async Task<List<GroupClaim>> Handle(GetInternalGroupClaimsWithGroupIdQuery request, CancellationToken cancellationToken)
            {
                return await _groupClaimRepository.GetListAsync(s=>s.GroupId == request.GroupId);
            }
        }
    }
}