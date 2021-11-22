using Circle.Core.Utilities.Messages;
using Circle.Core.Utilities.Results;
using Circle.Library.Business.BusinessAspects;
using Circle.Library.Business.Helpers;
using Circle.Library.DataAccess.Abstract;
using Circle.Library.Entities.ComplexTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Circle.Library.Business.Handlers.Groups.Queries
{
    public class GetGroupWithClaimsQuery : IRequest<ResponseMessage<List<GroupModel>>>
    {
        public Guid GroupId { get; set; }

        public class GetGroupWithClaimsQueryHandler : IRequestHandler<GetGroupWithClaimsQuery, ResponseMessage<List<GroupModel>>>
        {
            private readonly IGroupRepository _groupRepository;
            private readonly IReturnUtility _returnUtility;
            //private readonly IMediator _mediator;
            IGroupClaimRepository _groupClaimRepository;
            IOperationClaimRepository _operationClaimRepository;

            public GetGroupWithClaimsQueryHandler(
                IGroupRepository groupRepository,
                IGroupClaimRepository groupClaimRepository,
                IOperationClaimRepository operationClaimRepository,
                IReturnUtility returnUtility)
            {
                _groupRepository = groupRepository;
                _returnUtility = returnUtility;
                _groupClaimRepository = groupClaimRepository;
                _operationClaimRepository = operationClaimRepository;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<List<GroupModel>>> Handle(GetGroupWithClaimsQuery request, CancellationToken cancellationToken)
            {
                List<GroupModel> result = new List<GroupModel>();
                var list = await _groupRepository.GetListAsync(x => (x.LanguageId == LanguageExtension.LanguageId) && (x.Id == request.GroupId || request.GroupId == Guid.Empty));
                var groupClaim = await _groupClaimRepository.GetListAsync(x => x.GroupId == request.GroupId || request.GroupId == Guid.Empty);
                var operationClaim = await _operationClaimRepository.GetListAsync(x=> x.LanguageId == LanguageExtension.LanguageId);
                result = list.Select
                    (x => new GroupModel
                    {
                        Group = x
                        ,
                        GroupClaims = groupClaim.Where(x2 => x2.GroupId == x.Id).
                        Select(x3 => new GroupClaimModel
                        {
                            Id = x3.Id
                        ,
                            GroupId = x3.GroupId
                        ,
                            OperationClaimId = x3.OperationClaimId
                        ,
                            Description = operationClaim.Where(x4 => x4.Id == x3.OperationClaimId && x4.LanguageId == x.LanguageId).Count() > 0 ? operationClaim.Where(x4 => x4.Id == x3.OperationClaimId && x4.LanguageId == x.LanguageId).FirstOrDefault().Description : (operationClaim.Where(x4 => x4.Id == x3.OperationClaimId).Count() > 0 ? operationClaim.Where(x4 => x4.Id == x3.OperationClaimId).FirstOrDefault().Description : "")
                        }).ToList()
                    }).ToList();

                if (result == null || result.Count() <= 0)
                {
                    return await _returnUtility.NoDataFound<List<GroupModel>>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                return _returnUtility.SuccessData(result);
            }
        }
    }
}
