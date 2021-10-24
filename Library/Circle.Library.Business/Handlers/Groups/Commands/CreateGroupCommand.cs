using System.Threading;
using System.Threading.Tasks;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Messages;
using Circle.Core.Utilities.Results;
using Circle.Library.Business.BusinessAspects;
using Circle.Library.Business.Helpers;
using Circle.Library.DataAccess.Abstract;
using MediatR;

namespace Circle.Library.Business.Handlers.Groups.Commands
{
    public class CreateGroupCommand : IRequest<ResponseMessage<Group>>
    {
        public string GroupName { get; set; }

        public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, ResponseMessage<Group>>
        {
            private readonly IGroupRepository _groupRepository;
            private readonly IReturnUtility _returnUtility;

            public CreateGroupCommandHandler(IGroupRepository groupRepository, IReturnUtility returnUtility)
            {
                _groupRepository = groupRepository;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<Group>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
            {
                var group = await _groupRepository.GetAsync(s => s.GroupName == request.GroupName.Trim().ToUpper());
                if (group != null)
                {
                    return await _returnUtility.Fail<Group>(MessageDefinitions.KAYIT_ZATEN_MEVCUT);
                }

                group = new Group
                {
                    GroupName = request.GroupName
                };
                _groupRepository.Add(group);
                await _groupRepository.SaveChangesAsync();

                return await _returnUtility.SuccessWithData(MessageDefinitions.KAYIT_ISLEMI_BASARILI, group);
            }
        }
    }
}