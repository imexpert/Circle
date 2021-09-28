using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;

namespace Circle.Library.Business.Handlers.Groups.Queries
{
    public class GetGroupQuery : IRequest<IDataResult<Group>>
    {
        public int GroupId { get; set; }

        public class GetGroupQueryHandler : IRequestHandler<GetGroupQuery, IDataResult<Group>>
        {
            private readonly IGroupRepository _groupRepository;

            public GetGroupQueryHandler(IGroupRepository groupRepository)
            {
                _groupRepository = groupRepository;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Group>> Handle(GetGroupQuery request, CancellationToken cancellationToken)
            {
                var group = await _groupRepository.GetAsync(x => x.Id == request.GroupId);

                return new SuccessDataResult<Group>(group);
            }
        }
    }
}