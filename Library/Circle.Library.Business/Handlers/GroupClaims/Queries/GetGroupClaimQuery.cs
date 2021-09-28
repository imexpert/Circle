using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;

namespace Circle.Library.Business.Handlers.GroupClaims.Queries
{
    public class GetGroupClaimQuery : IRequest<IDataResult<GroupClaim>>
    {
        public Guid Id { get; set; }

        public class GetGroupClaimQueryHandler : IRequestHandler<GetGroupClaimQuery, IDataResult<GroupClaim>>
        {
            private readonly IGroupClaimRepository _groupClaimRepository;

            public GetGroupClaimQueryHandler(IGroupClaimRepository groupClaimRepository)
            {
                _groupClaimRepository = groupClaimRepository;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IDataResult<GroupClaim>> Handle(GetGroupClaimQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<GroupClaim>(
                    await _groupClaimRepository.GetAsync(x => x.GroupId == request.Id));
            }
        }
    }
}