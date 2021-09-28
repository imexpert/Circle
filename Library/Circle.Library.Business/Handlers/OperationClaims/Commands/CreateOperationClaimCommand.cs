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

namespace Circle.Library.Business.Handlers.OperationClaims.Commands
{
    public class CreateOperationClaimCommand : IRequest<IResult>
    {
        public string ClaimName { get; set; }

        public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, IResult>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;

            public CreateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository)
            {
                _operationClaimRepository = operationClaimRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IResult> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                if (IsClaimExists(request.ClaimName))
                {
                    return new ErrorResult(Messages.OperationClaimExists);
                }

                var operationClaim = new OperationClaim
                {
                    Name = request.ClaimName
                };
                _operationClaimRepository.Add(operationClaim);
                await _operationClaimRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Added);
            }

            private bool IsClaimExists(string claimName)
            {
                return _operationClaimRepository.Query().Any(x => x.Name == claimName);
            }
        }
    }
}