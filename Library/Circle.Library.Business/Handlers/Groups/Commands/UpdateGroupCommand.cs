using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using Circle.Library.Business.Helpers;
using Circle.Core.Utilities.Messages;

namespace Circle.Library.Business.Handlers.Groups.Commands
{
    public class UpdateGroupCommand : IRequest<ResponseMessage<Group>>
    {
        public Group Group { get; set; }

        public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, ResponseMessage<Group>>
        {
            private readonly IGroupRepository _groupRepository;
            private readonly IReturnUtility _returnUtility;

            public UpdateGroupCommandHandler(IGroupRepository groupRepository, IReturnUtility returnUtility)
            {
                _groupRepository = groupRepository;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<Group>> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
            {
                var group = await _groupRepository.GetAsync(s => s.Id == request.Group.Id);
                if (group == null)
                {
                    return await _returnUtility.NoDataFound<Group>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                group.GroupName = request.Group.GroupName;

                _groupRepository.Update(group);
                await _groupRepository.SaveChangesAsync();

                return await _returnUtility.SuccessWithData(MessageDefinitions.GUNCELLEME_ISLEMI_BASARILI, group);
            }
        }
    }
}