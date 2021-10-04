﻿using System.Threading;
using System.Threading.Tasks;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;

using Circle.Library.Entities.ComplexTypes;
using Circle.Library.Business.Helpers;
using Circle.Core.Utilities.Messages;

namespace Circle.Library.Business.Handlers.GroupClaims.Commands
{
    public class CreateGroupClaimCommand : IRequest<ResponseMessage<GroupClaim>>
    {
        public GroupClaim Model { get; set; }

        public class CreateGroupClaimCommandHandler : IRequestHandler<CreateGroupClaimCommand, ResponseMessage<GroupClaim>>
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

            public async Task<ResponseMessage<GroupClaim>> Handle(CreateGroupClaimCommand request, CancellationToken cancellationToken)
            {
                var groupClaim = new GroupClaim
                {
                    OperationClaimId = request.Model.OperationClaimId,
                    GroupId = request.Model.GroupId
                };

                var isGroupExists = await _groupRepository.GetAsync(s => s.Id == groupClaim.GroupId);
                if (isGroupExists == null || isGroupExists.Id == System.Guid.Empty)
                {
                    return await _returnUtility.Fail<GroupClaim>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                var isOperationExists = await _operationClaimRepository.GetAsync(s => s.Id == groupClaim.OperationClaimId);
                if (isOperationExists == null || isOperationExists.Id == System.Guid.Empty)
                {
                    return await _returnUtility.Fail<GroupClaim>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                _groupClaimRepository.Add(groupClaim);
                await _groupClaimRepository.SaveChangesAsync();

                return await _returnUtility.SuccessWithData(MessageDefinitions.KAYIT_ISLEMI_BASARILI, groupClaim);
            }
        }
    }
}