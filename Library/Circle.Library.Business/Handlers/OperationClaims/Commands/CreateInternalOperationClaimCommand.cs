using System.Threading;
using System.Threading.Tasks;
using Circle.Core.Entities.Concrete;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using Circle.Library.Business.Helpers;
using System.Collections.Generic;

namespace Circle.Library.Business.Handlers.OperationClaims.Commands
{
    public class CreateInternalOperationClaimCommand : IRequest<bool>
    {
        public List<OperationClaim> ModelList { get; set; }

        public class CreateInternalOperationClaimCommandHandler : IRequestHandler<CreateInternalOperationClaimCommand, bool>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IReturnUtility _returnUtility;

            public CreateInternalOperationClaimCommandHandler(
                IOperationClaimRepository operationClaimRepository, 
                IReturnUtility returnUtility)
            {
                _operationClaimRepository = operationClaimRepository;
                _returnUtility = returnUtility;
            }

            public async Task<bool> Handle(CreateInternalOperationClaimCommand request, CancellationToken cancellationToken)
            {
                foreach (var item in request.ModelList)
                {
                    _operationClaimRepository.Add(item);
                    await _operationClaimRepository.SaveChangesAsync();
                }
                

                return true;
            }
        }
    }
}