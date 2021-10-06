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
using Circle.Library.Business.BusinessAspects;

namespace Circle.Library.Business.Handlers.OperationClaims.Commands
{
    public class CreateOperationClaimCommand : IRequest<ResponseMessage<OperationClaim>>
    {
        public OperationClaim Model { get; set; }

        public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, ResponseMessage<OperationClaim>>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IReturnUtility _returnUtility;

            public CreateOperationClaimCommandHandler(
                IOperationClaimRepository operationClaimRepository, 
                IReturnUtility returnUtility)
            {
                _operationClaimRepository = operationClaimRepository;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<OperationClaim>> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                if (IsClaimExists(request.Model.Name))
                {
                    return await _returnUtility.Fail<OperationClaim>(MessageDefinitions.KAYIT_ZATEN_MEVCUT);
                }

                _operationClaimRepository.Add(request.Model);
                await _operationClaimRepository.SaveChangesAsync();

                return await _returnUtility.SuccessWithData(MessageDefinitions.KAYIT_ISLEMI_BASARILI, request.Model);
            }

            private bool IsClaimExists(string claimName)
            {
                return _operationClaimRepository.Query().Any(x => x.Name == claimName);
            }
        }
    }
}