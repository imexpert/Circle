using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;
using Circle.Library.Business.Helpers;
using Circle.Core.Utilities.Messages;

namespace Circle.Library.Business.Handlers.OperationClaims.Commands
{
    public class DeleteOperationClaimCommand : IRequest<ResponseMessage<NoContent>>
    {
        public Guid Id { get; set; }

        public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, ResponseMessage<NoContent>>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IReturnUtility _returnUtility;

            public DeleteOperationClaimCommandHandler(
                IOperationClaimRepository operationClaimRepository, 
                IReturnUtility returnUtility)
            {
                _operationClaimRepository = operationClaimRepository;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<NoContent>> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
            {
                var claimToDelete = await _operationClaimRepository.GetAsync(x => x.Id == request.Id);
                if (claimToDelete == null || claimToDelete.Id == Guid.Empty)
                {
                    return await _returnUtility.NoDataFound<NoContent>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                _operationClaimRepository.Delete(claimToDelete);
                await _operationClaimRepository.SaveChangesAsync();

                return await _returnUtility.Success<NoContent>(MessageDefinitions.SILME_ISLEMI_BASARILI);
            }
        }
    }
}