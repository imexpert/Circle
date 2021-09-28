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

namespace Circle.Library.Business.Handlers.OperationClaims.Queries
{
    public class GetOperationClaimsQuery : IRequest<IDataResult<IEnumerable<OperationClaim>>>
    {
        public class
            GetOperationClaimsQueryHandler : IRequestHandler<GetOperationClaimsQuery,
                IDataResult<IEnumerable<OperationClaim>>>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;

            public GetOperationClaimsQueryHandler(IOperationClaimRepository operationClaimRepository)
            {
                _operationClaimRepository = operationClaimRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheAspect(10)]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IDataResult<IEnumerable<OperationClaim>>> Handle(GetOperationClaimsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OperationClaim>>(
                    await _operationClaimRepository.GetListAsync());
            }
        }
    }
}