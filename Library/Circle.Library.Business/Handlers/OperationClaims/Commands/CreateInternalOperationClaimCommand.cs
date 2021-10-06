using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using Circle.Library.Entities.ComplexTypes;
using Circle.Library.Business.Helpers;
using Circle.Core.Utilities.Messages;
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
                _operationClaimRepository.AddRange(request.ModelList);
                await _operationClaimRepository.SaveChangesAsync();

                return true;
            }
        }
    }
}