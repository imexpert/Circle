using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using Circle.Library.Business.Helpers;
using System.Linq;
using Circle.Core.Utilities.Messages;

namespace Circle.Library.Business.Handlers.OperationClaims.Queries
{
    public class GetOperationClaimsQuery : IRequest<ResponseMessage<List<OperationClaim>>>
    {
        public class
            GetOperationClaimsQueryHandler : IRequestHandler<GetOperationClaimsQuery,
                ResponseMessage<List<OperationClaim>>>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IReturnUtility _returnUtility;

            public GetOperationClaimsQueryHandler(IOperationClaimRepository operationClaimRepository, IReturnUtility returnUtility)
            {
                _operationClaimRepository = operationClaimRepository;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<List<OperationClaim>>> Handle(GetOperationClaimsQuery request, CancellationToken cancellationToken)
            {
                var list = await _operationClaimRepository.GetListAsync();
                if (list == null || list.Count() <= 0)
                {
                    return await _returnUtility.NoDataFound<List<OperationClaim>>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                return _returnUtility.SuccessData(list);
            }
        }
    }
}