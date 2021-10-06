using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;
using Circle.Library.Business.Helpers;
using Circle.Core.Utilities.Messages;

namespace Circle.Library.Business.Handlers.GroupClaims.Queries
{
    public class GetGroupClaimQuery : IRequest<ResponseMessage<GroupClaim>>
    {
        public Guid Id { get; set; }

        public class GetGroupClaimQueryHandler : IRequestHandler<GetGroupClaimQuery, ResponseMessage<GroupClaim>>
        {
            private readonly IGroupClaimRepository _groupClaimRepository;
            private readonly IReturnUtility _returnUtility;

            public GetGroupClaimQueryHandler(IGroupClaimRepository groupClaimRepository, IReturnUtility returnUtility)
            {
                _groupClaimRepository = groupClaimRepository;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<GroupClaim>> Handle(GetGroupClaimQuery request, CancellationToken cancellationToken)
            {
                var groupClaim = await _groupClaimRepository.GetAsync(x => x.Id == request.Id);

                if (groupClaim == null)
                {
                    return await _returnUtility.NoDataFound<GroupClaim>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                return _returnUtility.SuccessData(groupClaim);
            }
        }
    }
}