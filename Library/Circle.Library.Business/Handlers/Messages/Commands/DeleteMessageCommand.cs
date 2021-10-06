using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Messages;
using Circle.Core.Utilities.Results;
using Circle.Library.Business.BusinessAspects;
using Circle.Library.Business.Helpers;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Circle.Library.Business.Handlers.Messages.Commands
{
    public class DeleteMessageCommand : IRequest<ResponseMessage<NoContent>>
    {
        public Guid Id { get; set; }

        public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, ResponseMessage<NoContent>>
        {
            private readonly IMessageRepository _messageRepository;
            private readonly IReturnUtility _returnUtility;

            public DeleteMessageCommandHandler(IMessageRepository messageRepository, IReturnUtility returnUtility)
            {
                _messageRepository = messageRepository;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<NoContent>> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
            {
                var message = await _messageRepository.GetAsync(s => s.Id == request.Id);
                if (message == null)
                {
                    return await _returnUtility.Fail<NoContent>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                _messageRepository.Delete(message);
                await _messageRepository.SaveChangesAsync();

                return await _returnUtility.Success<NoContent>(MessageDefinitions.SILME_ISLEMI_BASARILI);
            }
        }
    }
}
