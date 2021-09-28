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

namespace Circle.Library.Business.Handlers.GroupClaims.Commands
{
    public class DeleteGroupClaimCommand : IRequest<IResult>
    {
        public Guid Id { get; set; }

        public class DeleteGroupClaimCommandHandler : IRequestHandler<DeleteGroupClaimCommand, IResult>
        {
            private readonly IGroupClaimRepository _groupClaimRepository;

            public DeleteGroupClaimCommandHandler(IGroupClaimRepository groupClaimRepository)
            {
                _groupClaimRepository = groupClaimRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IResult> Handle(DeleteGroupClaimCommand request, CancellationToken cancellationToken)
            {
                var groupClaimToDelete = await _groupClaimRepository.GetAsync(x => x.GroupId == request.Id);

                _groupClaimRepository.Delete(groupClaimToDelete);
                await _groupClaimRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}