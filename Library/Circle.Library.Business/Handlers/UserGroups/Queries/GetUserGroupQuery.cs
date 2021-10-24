using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;
using Circle.Library.Business.Helpers;
using Circle.Core.Utilities.Messages;

namespace Circle.Library.Business.Handlers.UserGroups.Queries
{
    public class GetUserGroupQuery : IRequest<ResponseMessage<UserGroup>>
    {
        public Guid Id { get; set; }

        public class GetUserGroupQueryHandler : IRequestHandler<GetUserGroupQuery, ResponseMessage<UserGroup>>
        {
            private readonly IUserGroupRepository _repository;
            private readonly IReturnUtility _returnUtility;

            public GetUserGroupQueryHandler(
                IUserGroupRepository repository,
                IReturnUtility returnUtility)
            {
                _repository = repository;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<UserGroup>> Handle(GetUserGroupQuery request, CancellationToken cancellationToken)
            {
                var group = await _repository.GetAsync(x => x.Id == request.Id);

                if (group == null)
                {
                    return await _returnUtility.NoDataFound<UserGroup>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                return _returnUtility.SuccessData(group);
            }
        }
    }
}