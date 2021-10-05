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
    public class GetGroupClaimsWithGroupIdQuery : IRequest<ResponseMessage<List<GroupClaim>>>
    {
        public Guid GroupId { get; set; }
        public class GetGroupClaimsWithGroupIdQueryHandler : IRequestHandler<GetGroupClaimsWithGroupIdQuery, ResponseMessage<List<GroupClaim>>>
        {
            private readonly IGroupClaimRepository _groupClaimRepository;
            private readonly IReturnUtility _returnUtility;

            public GetGroupClaimsWithGroupIdQueryHandler(IGroupClaimRepository groupClaimRepository, IReturnUtility returnUtility)
            {
                _groupClaimRepository = groupClaimRepository;
                _returnUtility = returnUtility;
            }

            public async Task<ResponseMessage<List<GroupClaim>>> Handle(GetGroupClaimsWithGroupIdQuery request, CancellationToken cancellationToken)
            {
                var list = await _groupClaimRepository.GetListAsync(s=>s.GroupId == request.GroupId);
                if (list == null || list.Count() <= 0)
                {
                    return await _returnUtility.NoDataFound<List<GroupClaim>>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                return _returnUtility.SuccessData(list);
            }
        }
    }
}