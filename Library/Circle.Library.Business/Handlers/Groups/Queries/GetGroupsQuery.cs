using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using Circle.Library.Business.Helpers;
using Circle.Core.Utilities.Messages;

namespace Circle.Library.Business.Handlers.Groups.Queries
{
    public class GetGroupsQuery : IRequest<ResponseMessage<List<Group>>>
    {
        public int Id { get; set; }

        public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, ResponseMessage<List<Group>>>
        {
            private readonly IGroupRepository _groupRepository;
            private readonly IReturnUtility _returnUtility;

            public GetGroupsQueryHandler(
                IGroupRepository groupRepository,
                IReturnUtility returnUtility)
            {
                _groupRepository = groupRepository;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<List<Group>>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
            {
                var list = await _groupRepository.GetListAsync();
                if (list == null || list.Count() <= 0)
                {
                    return await _returnUtility.NoDataFound<List<Group>>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                return _returnUtility.SuccessData(list);
            }
        }
    }
}