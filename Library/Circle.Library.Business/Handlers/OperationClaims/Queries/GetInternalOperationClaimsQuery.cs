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

namespace Circle.Library.Business.Handlers.OperationClaims.Queries
{
    public class GetInternalOperationClaimsQuery : IRequest<List<OperationClaim>>
    {
        public class
            GetInternalOperationClaimsQueryHandler : IRequestHandler<GetInternalOperationClaimsQuery,
                List<OperationClaim>>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IReturnUtility _returnUtility;

            public GetInternalOperationClaimsQueryHandler(IOperationClaimRepository operationClaimRepository, IReturnUtility returnUtility)
            {
                _operationClaimRepository = operationClaimRepository;
                _returnUtility = returnUtility;
            }

            public async Task<List<OperationClaim>> Handle(GetInternalOperationClaimsQuery request, CancellationToken cancellationToken)
            {
                return await _operationClaimRepository.GetListAsync();
            }
        }
    }
}