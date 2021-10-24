using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Circle.Core.Entities.Concrete;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using Circle.Library.Business.Helpers;

namespace Circle.Library.Business.Handlers.Groups.Queries
{
    public class GetInternalGroupsQuery : IRequest<List<Group>>
    {
        public int Id { get; set; }

        public class GetInternalGroupsQueryHandler : IRequestHandler<GetInternalGroupsQuery, 
            List<Group>>
        {
            private readonly IGroupRepository _groupRepository;
            private readonly IReturnUtility _returnUtility;

            public GetInternalGroupsQueryHandler(
                IGroupRepository groupRepository,
                IReturnUtility returnUtility)
            {
                _groupRepository = groupRepository;
                _returnUtility = returnUtility;
            }

            public async Task<List<Group>> Handle(GetInternalGroupsQuery request, CancellationToken cancellationToken)
            {
                return await _groupRepository.GetListAsync();
            }
        }
    }
}