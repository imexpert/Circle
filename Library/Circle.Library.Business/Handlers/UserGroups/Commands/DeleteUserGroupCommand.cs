using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;

using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;
using Circle.Library.Business.Helpers;
using Circle.Core.Utilities.Messages;

namespace Circle.Library.Business.Handlers.UserGroups.Commands
{
    public class DeleteUserGroupCommand : IRequest<ResponseMessage<NoContent>>
    {
        public Guid Id { get; set; }

        public class DeleteUserGroupCommandHandler : IRequestHandler<DeleteUserGroupCommand, ResponseMessage<NoContent>>
        {
            private readonly IUserGroupRepository _groupRepository;
            private readonly IReturnUtility _returnUtility;

            public DeleteUserGroupCommandHandler(
                IUserGroupRepository groupRepository,
                IReturnUtility returnUtility)
            {
                _groupRepository = groupRepository;
                _returnUtility = returnUtility;
            }

            public async Task<ResponseMessage<NoContent>> Handle(DeleteUserGroupCommand request, CancellationToken cancellationToken)
            {
                var userGroupToDelete = await _groupRepository.GetAsync(x => x.Id == request.Id);

                if (userGroupToDelete == null || userGroupToDelete.Id == Guid.Empty)
                {
                    return await _returnUtility.NoDataFound<NoContent>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                _groupRepository.Delete(userGroupToDelete);
                await _groupRepository.SaveChangesAsync();

                return await _returnUtility.Success<NoContent>(MessageDefinitions.SILME_ISLEMI_BASARILI);
            }
        }
    }
}