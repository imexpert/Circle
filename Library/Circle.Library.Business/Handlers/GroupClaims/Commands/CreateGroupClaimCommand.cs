using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using Circle.Library.Business.Constants;

namespace Circle.Library.Business.Handlers.GroupClaims.Commands
{
    public class CreateGroupClaimCommand : IRequest<IResult>
    {
        public string ClaimName { get; set; }

        public class CreateGroupClaimCommandHandler : IRequestHandler<CreateGroupClaimCommand, IResult>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;

            public CreateGroupClaimCommandHandler(IOperationClaimRepository operationClaimRepository)
            {
                _operationClaimRepository = operationClaimRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IResult> Handle(CreateGroupClaimCommand request, CancellationToken cancellationToken)
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
                return !(_operationClaimRepository.Get(x => x.Name == claimName) is null);
            }
        }
    }
}