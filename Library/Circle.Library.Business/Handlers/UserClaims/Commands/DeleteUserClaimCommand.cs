using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Library.Business.Constants;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;

namespace Circle.Library.Business.Handlers.UserClaims.Commands
{
    public class DeleteUserClaimCommand : IRequest<IResult>
    {
        public Guid Id { get; set; }

        public class DeleteUserClaimCommandHandler : IRequestHandler<DeleteUserClaimCommand, IResult>
        {
            private readonly IUserClaimRepository _userClaimRepository;

            public DeleteUserClaimCommandHandler(IUserClaimRepository userClaimRepository)
            {
                _userClaimRepository = userClaimRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IResult> Handle(DeleteUserClaimCommand request, CancellationToken cancellationToken)
            {
                var entityToDelete = await _userClaimRepository.GetAsync(x => x.UserId == request.Id);

                _userClaimRepository.Delete(entityToDelete);
                await _userClaimRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}