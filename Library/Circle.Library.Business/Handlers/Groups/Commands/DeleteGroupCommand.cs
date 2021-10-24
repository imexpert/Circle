using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;
using Circle.Core.Utilities.Messages;
using Circle.Library.Business.Helpers;

namespace Circle.Library.Business.Handlers.Groups.Commands
{
    public class DeleteGroupCommand : IRequest<ResponseMessage<NoContent>>
    {
        public Guid Id { get; set; }

        public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, ResponseMessage<NoContent>>
        {
            private readonly IGroupRepository _groupRepository;
            private readonly IReturnUtility _returnUtility;

            public DeleteGroupCommandHandler(
                IGroupRepository groupRepository,
                IReturnUtility returnUtility)
            {
                _groupRepository = groupRepository;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<NoContent>> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
            {
                var groupToDelete = await _groupRepository.GetAsync(x => x.Id == request.Id);

                if (groupToDelete == null || groupToDelete.Id == Guid.Empty)
                {
                    return await _returnUtility.NoDataFound<NoContent>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                _groupRepository.Delete(groupToDelete);
                await _groupRepository.SaveChangesAsync();

                return await _returnUtility.Success<NoContent>(MessageDefinitions.SILME_ISLEMI_BASARILI);
            }
        }
    }
}