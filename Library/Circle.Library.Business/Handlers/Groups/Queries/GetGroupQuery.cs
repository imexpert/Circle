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
using System.Collections.Generic;

namespace Circle.Library.Business.Handlers.Groups.Queries
{
    public class GetGroupQuery : IRequest<ResponseMessage<List<Group>>>
    {
        public Guid GroupId { get; set; }

        public class GetGroupQueryHandler : IRequestHandler<GetGroupQuery, ResponseMessage<List<Group>>>
        {
            private readonly IGroupRepository _groupRepository;
            private readonly IReturnUtility _returnUtility;

            public GetGroupQueryHandler(
                IGroupRepository groupRepository,
                IReturnUtility returnUtility)
            {
                _groupRepository = groupRepository;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<List<Group>>> Handle(GetGroupQuery request, CancellationToken cancellationToken)
            {
                var group = await _groupRepository.GetListAsync(x => x.Id == request.GroupId && x.LanguageId == LanguageExtension.LanguageId);

                if (group == null)
                {
                    return await _returnUtility.NoDataFound<List<Group>>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                return _returnUtility.SuccessData(group);
            }
        }
    }
}