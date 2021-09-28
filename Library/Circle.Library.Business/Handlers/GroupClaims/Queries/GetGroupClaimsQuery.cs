using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;

namespace Circle.Library.Business.Handlers.GroupClaims.Queries
{
    public class GetGroupClaimsQuery : IRequest<IDataResult<IEnumerable<GroupClaim>>>
    {
        public class
            GetGroupClaimsQueryHandler : IRequestHandler<GetGroupClaimsQuery, IDataResult<IEnumerable<GroupClaim>>>
        {
            private readonly IGroupClaimRepository _groupClaimRepository;

            public GetGroupClaimsQueryHandler(IGroupClaimRepository groupClaimRepository)
            {
                _groupClaimRepository = groupClaimRepository;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(MsSqlLogger))]
            [CacheAspect(10)]
            public async Task<IDataResult<IEnumerable<GroupClaim>>> Handle(GetGroupClaimsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<GroupClaim>>(await _groupClaimRepository.GetListAsync());
            }
        }
    }
}