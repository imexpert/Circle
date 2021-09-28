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

namespace Circle.Library.Business.Handlers.GroupClaims.Commands
{
    public class UpdateGroupClaimCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int[] ClaimIds { get; set; }

        public class UpdateGroupClaimCommandHandler : IRequestHandler<UpdateGroupClaimCommand, IResult>
        {
            private readonly IGroupClaimRepository _groupClaimRepository;

            public UpdateGroupClaimCommandHandler(IGroupClaimRepository groupClaimRepository)
            {
                _groupClaimRepository = groupClaimRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IResult> Handle(UpdateGroupClaimCommand request, CancellationToken cancellationToken)
            {
                var list = request.ClaimIds.Select(x => new GroupClaim() { ClaimId = x, GroupId = request.GroupId });

                await _groupClaimRepository.BulkInsert(request.GroupId, list);
                await _groupClaimRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}