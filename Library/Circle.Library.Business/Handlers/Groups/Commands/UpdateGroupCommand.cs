using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;

using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;
using Circle.Library.Business.Helpers;
using Circle.Core.Utilities.Messages;
using Circle.Core.Aspects.Autofac.Transaction;

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

            [TransactionScopeAspectAsync]
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