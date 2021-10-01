using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Library.Business.Constants;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using Circle.Library.Entities.ComplexTypes;

namespace Circle.Library.Business.Handlers.OperationClaims.Commands
{
    public class CreateOperationClaimCommand : IRequest<ResponseMessage<OperationClaim>>
    {
        public CreateOperationClaimModel Model { get; set; }

        public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, ResponseMessage<OperationClaim>>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;

            public CreateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository)
            {
                _operationClaimRepository = operationClaimRepository;
            }

            //[SecuredOperation(Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<ResponseMessage<OperationClaim>> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                if (IsClaimExists(request.Model.Name))
                {
                    return ResponseMessage<OperationClaim>.Fail("Method zaten kayıtlı.");
                }

                var operationClaim = new OperationClaim
                {
                    Name = request.Model.Name,
                    Alias = request.Model.Alias,
                    Description = request.Model.Description
                };

                _operationClaimRepository.Add(operationClaim);
                await _operationClaimRepository.SaveChangesAsync();

                return ResponseMessage<OperationClaim>.Success(operationClaim);
            }

            private bool IsClaimExists(string claimName)
            {
                return _operationClaimRepository.Query().Any(x => x.Name == claimName);
            }
        }
    }
}