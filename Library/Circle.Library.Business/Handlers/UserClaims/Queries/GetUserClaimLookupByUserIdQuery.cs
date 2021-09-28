using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Dtos;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;

namespace Circle.Library.Business.Handlers.UserClaims.Queries
{
    public class GetUserClaimLookupByUserIdQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
    {
        public int Id { get; set; }

        public class GetUserClaimLookupByUserIdQueryHandler : IRequestHandler<GetUserClaimLookupByUserIdQuery,
            IDataResult<IEnumerable<SelectionItem>>>
        {
            private readonly IUserClaimRepository _userClaimRepository;
            private readonly IMediator _mediator;

            public GetUserClaimLookupByUserIdQueryHandler(IUserClaimRepository userClaimRepository, IMediator mediator)
            {
                _userClaimRepository = userClaimRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetUserClaimLookupByUserIdQuery request, CancellationToken cancellationToken)
            {
                var data = await _userClaimRepository.GetUserClaimSelectedList(request.Id);
                return new SuccessDataResult<IEnumerable<SelectionItem>>(data);
            }
        }
    }
}