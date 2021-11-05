using System.Threading;
using System.Threading.Tasks;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using Circle.Library.Business.Helpers;
using Circle.Core.Utilities.Messages;
using Circle.Library.Business.BusinessAspects;
using System.Collections.Generic;
using Circle.Library.Entities.ComplexTypes;

namespace Circle.Library.Business.Handlers.GroupClaims.Commands
{
    public class CreateGroupClaimCommand : IRequest<ResponseMessage<List<GroupClaim>>>
    {
        public GroupModel Model { get; set; }

        public class CreateGroupClaimCommandHandler : IRequestHandler<CreateGroupClaimCommand, ResponseMessage<List<GroupClaim>>>
        {
            private readonly IGroupClaimRepository _groupClaimRepository;
            private readonly IGroupRepository _groupRepository;
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IReturnUtility _returnUtility;

            public CreateGroupClaimCommandHandler(IGroupClaimRepository groupClaimRepository, IGroupRepository groupRepository, IOperationClaimRepository operationClaimRepository, IReturnUtility returnUtility)
            {
                _groupClaimRepository = groupClaimRepository;
                _groupRepository = groupRepository;
                _operationClaimRepository = operationClaimRepository;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<List<GroupClaim>>> Handle(CreateGroupClaimCommand request, CancellationToken cancellationToken)
            {
                var isGroupExists = await _groupRepository.GetAsync(s => s.Id == request.Model.Group.Id);
                if (isGroupExists == null || isGroupExists.Id == System.Guid.Empty)
                {
                    return await _returnUtility.Fail<List<GroupClaim>>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                var groupClaimsToDelete = await _groupClaimRepository.GetListAsync(x => x.GroupId == isGroupExists.Id);
                if (groupClaimsToDelete.Count > 0)
                {
                    _groupClaimRepository.DeleteRange(groupClaimsToDelete);
                    await _groupClaimRepository.SaveChangesAsync();
                }
                List<GroupClaim> groupClaims = new List<GroupClaim>();
                foreach (var item in request.Model.GroupClaims)
                {
                    var groupClaim = new GroupClaim
                    {
                        OperationClaimId = item.OperationClaimId,
                        GroupId = isGroupExists.Id
                    };
                    var isOperationExists = await _operationClaimRepository.GetAsync(s => s.Id == groupClaim.OperationClaimId);
                    if (isOperationExists == null || isOperationExists.Id == System.Guid.Empty)
                    {
                        return await _returnUtility.Fail<List<GroupClaim>>(MessageDefinitions.KAYIT_BULUNAMADI);
                    }
                    groupClaims.Add(_groupClaimRepository.Add(groupClaim));
                }
                await _groupClaimRepository.SaveChangesAsync();

                return await _returnUtility.SuccessWithData(MessageDefinitions.KAYIT_ISLEMI_BASARILI, groupClaims);
            }
        }
    }
}