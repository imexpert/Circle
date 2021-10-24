using System.Threading;
using System.Threading.Tasks;
using Circle.Core.Entities.Concrete;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System.Collections.Generic;

namespace Circle.Library.Business.Handlers.GroupClaims.Commands
{
    public class CreateInternalGroupClaimCommand : IRequest<bool>
    {
        public List<GroupClaim> ModelList { get; set; }

        public class CreateInternalGroupClaimCommandHandler : IRequestHandler<CreateInternalGroupClaimCommand, bool>
        {
            private readonly IGroupClaimRepository _groupClaimRepository;

            public CreateInternalGroupClaimCommandHandler(IGroupClaimRepository groupClaimRepository)
            {
                _groupClaimRepository = groupClaimRepository;
            }

            public async Task<bool> Handle(CreateInternalGroupClaimCommand request, CancellationToken cancellationToken)
            {
                _groupClaimRepository.AddRange(request.ModelList);
                await _groupClaimRepository.SaveChangesAsync();
                return true;
            }
        }
    }
}