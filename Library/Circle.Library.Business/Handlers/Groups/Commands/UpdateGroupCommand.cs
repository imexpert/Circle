using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using Circle.Library.Business.Helpers;
using Circle.Core.Utilities.Messages;
using Circle.Library.Entities.ComplexTypes;
using Circle.Library.Business.Handlers.GroupClaims.Commands;
using Circle.Core.Aspects.Autofac.Transaction;

namespace Circle.Library.Business.Handlers.Groups.Commands
{
    public class UpdateGroupCommand : IRequest<ResponseMessage<Group>>
    {
        public GroupModel Model { get; set; }

        public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, ResponseMessage<Group>>
        {
            private readonly IGroupRepository _groupRepository;
            private readonly IReturnUtility _returnUtility;
            private IMediator _mediator;

            public UpdateGroupCommandHandler(IGroupRepository groupRepository, IReturnUtility returnUtility, IMediator mediator)
            {
                _groupRepository = groupRepository;
                _returnUtility = returnUtility;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            //[TransactionScopeAspect(Priority = 2)]
            public async Task<ResponseMessage<Group>> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
            {
                GroupModel groupModel = new GroupModel();
                groupModel.Group = await _groupRepository.GetAsync(s => s.Id == request.Model.Group.Id && s.LanguageId == request.Model.Group.LanguageId);
                if (groupModel.Group == null)
                {
                    return await _returnUtility.NoDataFound<Group>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                groupModel.Group.GroupName = request.Model.Group.GroupName;
                _groupRepository.Update(groupModel.Group);
                await _groupRepository.SaveChangesAsync();

                #region ingilizce kayıt
                groupModel.GroupEn = await _groupRepository.GetAsync(s => s.Id == request.Model.GroupEn.Id && s.LanguageId == request.Model.GroupEn.LanguageId);
                if (groupModel.GroupEn != null)
                {
                    _groupRepository.Update(request.Model.GroupEn);
                    await _groupRepository.SaveChangesAsync();
                }
                #endregion

                if (request.Model.GroupClaims.Count > 0)
                {
                    groupModel.GroupClaims = request.Model.GroupClaims;
                    CreateGroupClaimCommand groupClaimCommand = new CreateGroupClaimCommand()
                    {
                        Model = groupModel
                    };

                    var groupClaims = await _mediator.Send(groupClaimCommand);
                }

                return await _returnUtility.SuccessWithData(MessageDefinitions.GUNCELLEME_ISLEMI_BASARILI, groupModel.Group);
            }
        }
    }
}