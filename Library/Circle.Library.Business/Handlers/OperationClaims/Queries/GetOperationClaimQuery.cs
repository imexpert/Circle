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

namespace Circle.Library.Business.Handlers.OperationClaims.Queries
{
    public class GetOperationClaimQuery : IRequest<IDataResult<OperationClaim>>
    {
        public Guid Id { get; set; }

        public class
            GetOperationClaimQueryHandler : IRequestHandler<GetOperationClaimQuery, IDataResult<OperationClaim>>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;

            public GetOperationClaimQueryHandler(IOperationClaimRepository operationClaimRepository)
            {
                _operationClaimRepository = operationClaimRepository;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IDataResult<OperationClaim>> Handle(GetOperationClaimQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<OperationClaim>(
                    await _operationClaimRepository.GetAsync(x => x.Id == request.Id));
            }
        }
    }
}