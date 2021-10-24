using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using Circle.Library.Business.Helpers;
using Circle.Core.Utilities.Messages;
using System.Linq;

namespace Circle.Library.Business.Handlers.UserGroups.Queries
{
    public class GetUserGroupsQuery : IRequest<ResponseMessage<List<UserGroup>>>
    {
        public int Id { get; set; }

        public class GetUserGroupsQueryHandler : IRequestHandler<GetUserGroupsQuery, ResponseMessage<List<UserGroup>>>
        {
            private readonly IUserGroupRepository _repository;
            private readonly IReturnUtility _returnUtility;

            public GetUserGroupsQueryHandler(
                IUserGroupRepository repository,
                IReturnUtility returnUtility)
            {
                _repository = repository;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<List<UserGroup>>> Handle(GetUserGroupsQuery request, CancellationToken cancellationToken)
            {
                var list = await _repository.GetListAsync();
                if (list == null || list.Count() <= 0)
                {
                    return await _returnUtility.NoDataFound<List<UserGroup>>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                return _returnUtility.SuccessData(list);
            }
        }
    }
}