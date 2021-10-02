using System.Threading;
using System.Threading.Tasks;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;

using Circle.Library.Entities.ComplexTypes;

namespace Circle.Library.Business.Handlers.GroupClaims.Commands
{
    public class CreateGroupClaimCommand : IRequest<ResponseMessage<GroupClaim>>
    {
        public CreateGroupClaimModel Model { get; set; }

        public class CreateGroupClaimCommandHandler : IRequestHandler<CreateGroupClaimCommand, ResponseMessage<GroupClaim>>
        {
            private readonly IGroupClaimRepository _groupClaimRepository;
            private readonly IGroupRepository _groupRepository;
            private readonly IOperationClaimRepository _operationClaimRepository;

            public CreateGroupClaimCommandHandler(
                IGroupClaimRepository groupClaimRepository, 
                IGroupRepository groupRepository,
                IOperationClaimRepository operationClaimRepository)
            {
                _groupClaimRepository = groupClaimRepository;
                _groupRepository = groupRepository;
                _operationClaimRepository = operationClaimRepository;
            }

            //[SecuredOperation(Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<ResponseMessage<GroupClaim>> Handle(CreateGroupClaimCommand request, CancellationToken cancellationToken)
            {
                var groupClaim = new GroupClaim
                {
                    OperationClaimId = request.Model.ClaimId,
                    GroupId = request.Model.GroupId
                };

                var isGroupExists = await _groupRepository.GetAsync(s => s.Id == groupClaim.GroupId);
                if (isGroupExists == null || isGroupExists.Id == System.Guid.Empty)
                {
                    return ResponseMessage<GroupClaim>.NoDataFound("Grup bulunamadı.");
                }

                var isOperationExists = await _operationClaimRepository.GetAsync(s => s.Id == groupClaim.OperationClaimId);
                if (isOperationExists == null || isOperationExists.Id == System.Guid.Empty)
                {
                    return ResponseMessage<GroupClaim>.NoDataFound("Operasyon tanımı bulunamadı.");
                }

                _groupClaimRepository.Add(groupClaim);
                await _groupClaimRepository.SaveChangesAsync();

                return ResponseMessage<GroupClaim>.Success(groupClaim);
            }
        }
    }
}