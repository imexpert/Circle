using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Library.Business.Constants;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Caching;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Circle.Library.Business.Handlers.UserClaims.Commands
{
    public class UpdateUserClaimCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int[] ClaimId { get; set; }


        public class UpdateUserClaimCommandHandler : IRequestHandler<UpdateUserClaimCommand, IResult>
        {
            private readonly IUserClaimRepository _userClaimRepository;
            private readonly ICacheManager _cacheManager;

            public UpdateUserClaimCommandHandler(IUserClaimRepository userClaimRepository, ICacheManager cacheManager)
            {
                _userClaimRepository = userClaimRepository;
                _cacheManager = cacheManager;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IResult> Handle(UpdateUserClaimCommand request, CancellationToken cancellationToken)
            {
                var userList = request.ClaimId.Select(x => new UserClaim() { ClaimId = x, UserId = request.UserId });

                await _userClaimRepository.BulkInsert(request.UserId, userList);
                await _userClaimRepository.SaveChangesAsync();

                _cacheManager.Remove($"{CacheKeys.UserIdForClaim}={request.UserId}");

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}