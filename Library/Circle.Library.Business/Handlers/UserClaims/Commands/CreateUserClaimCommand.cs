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
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;

namespace Circle.Library.Business.Handlers.UserClaims.Commands
{
    public class CreateUserClaimCommand : IRequest<IResult>
    {
        public Guid UserId { get; set; }
        public Guid ClaimId { get; set; }

        public class CreateUserClaimCommandHandler : IRequestHandler<CreateUserClaimCommand, IResult>
        {
            private readonly IUserClaimRepository _userClaimRepository;
            private readonly ICacheManager _cacheManager;

            public CreateUserClaimCommandHandler(IUserClaimRepository userClaimRepository, ICacheManager cacheManager)
            {
                _userClaimRepository = userClaimRepository;
                _cacheManager = cacheManager;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IResult> Handle(CreateUserClaimCommand request, CancellationToken cancellationToken)
            {
                var userClaim = new UserClaim
                {
                    ClaimId = request.ClaimId,
                    UserId = request.UserId
                };
                _userClaimRepository.Add(userClaim);
                await _userClaimRepository.SaveChangesAsync();

                _cacheManager.Remove($"{CacheKeys.UserIdForClaim}={request.UserId}");

                return new SuccessResult(Messages.Added);
            }
        }
    }
}