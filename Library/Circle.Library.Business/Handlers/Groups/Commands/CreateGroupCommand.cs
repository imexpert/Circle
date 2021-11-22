using System.Threading;
using System.Threading.Tasks;
using Circle.Core.Aspects.Autofac.Transaction;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Messages;
using Circle.Core.Utilities.Results;
using Circle.Library.Business.BusinessAspects;
using Circle.Library.Business.Handlers.GroupClaims.Commands;
using Circle.Library.Business.Helpers;
using Circle.Library.DataAccess.Abstract;
using Circle.Library.Entities.ComplexTypes;
using MediatR;

namespace Circle.Library.Business.Handlers.Groups.Commands
{
    public class CreateGroupCommand : IRequest<ResponseMessage<Group>>
    {
        public GroupModel Model { get; set; }

        public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, ResponseMessage<Group>>
        {
            private readonly IGroupRepository _groupRepository;
            private readonly IReturnUtility _returnUtility;
            private IMediator _mediator;

            public CreateGroupCommandHandler(IGroupRepository groupRepository, IReturnUtility returnUtility, IMediator mediator)
            {
                _groupRepository = groupRepository;
                _returnUtility = returnUtility;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            //[TransactionScopeAspect]
            public async Task<ResponseMessage<Group>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
            {
                GroupModel groupModel = new GroupModel();
                groupModel.Group = await _groupRepository.GetAsync(s => s.GroupName == request.Model.Group.GroupName.Trim().ToUpper());
                groupModel.GroupEn = await _groupRepository.GetAsync(s => s.GroupName == request.Model.GroupEn.GroupName.Trim().ToUpper());
                if (groupModel.Group != null)
                {
                    return await _returnUtility.Fail<Group>(MessageDefinitions.KAYIT_ZATEN_MEVCUT);
                }
                if (groupModel.GroupEn != null)
                {
                    return await _returnUtility.Fail<Group>(MessageDefinitions.KAYIT_ZATEN_MEVCUT);
                }
                //groupModel.Group = new Group
                //{
                //    GroupName = request.Model.Group.GroupName,
                //    LanguageId = LanguageExtension.LanguageId
                //};
                groupModel.Group = _groupRepository.Add(request.Model.Group);
                groupModel.GroupEn = _groupRepository.Add(request.Model.GroupEn);
                await _groupRepository.SaveChangesAsync();

                #region ingilizce kayıt
                groupModel.GroupEn = await _groupRepository.GetAsync(s => s.Id == request.Model.GroupEn.Id && s.LanguageId == request.Model.GroupEn.LanguageId);
                if (groupModel.GroupEn == null)
                {
                    groupModel.GroupEn= _groupRepository.Add(request.Model.GroupEn);
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

                return await _returnUtility.SuccessWithData(MessageDefinitions.KAYIT_ISLEMI_BASARILI, groupModel.Group);
            }
        }
    }
}