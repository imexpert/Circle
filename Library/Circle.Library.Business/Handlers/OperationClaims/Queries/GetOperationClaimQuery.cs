using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;
using Circle.Library.Business.Helpers;
using Circle.Core.Utilities.Messages;

namespace Circle.Library.Business.Handlers.OperationClaims.Queries
{
    public class GetOperationClaimQuery : IRequest<ResponseMessage<OperationClaim>>
    {
        public Guid Id { get; set; }

        public class
            GetOperationClaimQueryHandler : IRequestHandler<GetOperationClaimQuery, ResponseMessage<OperationClaim>>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IReturnUtility _returnUtility;

            public GetOperationClaimQueryHandler(IOperationClaimRepository operationClaimRepository, IReturnUtility returnUtility)
            {
                _operationClaimRepository = operationClaimRepository;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<OperationClaim>> Handle(GetOperationClaimQuery request, CancellationToken cancellationToken)
            {
                var operationClaim = await _operationClaimRepository.GetAsync(x => x.Id == request.Id);

                if (operationClaim == null)
                {
                    return await _returnUtility.NoDataFound<OperationClaim>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                return _returnUtility.SuccessData(operationClaim);
            }
        }
    }
}