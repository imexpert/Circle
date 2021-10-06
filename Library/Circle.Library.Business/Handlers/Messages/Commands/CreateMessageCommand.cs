using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Messages;
using Circle.Core.Utilities.Results;
using Circle.Library.Business.BusinessAspects;
using Circle.Library.Business.Helpers;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Circle.Library.Business.Handlers.Messages.Commands
{
    public class CreateMessageCommand : IRequest<ResponseMessage<Message>>
    {
        public Message Model { get; set; }

        public class CreateLanguageCommandHandler : IRequestHandler<CreateMessageCommand, ResponseMessage<Message>>
        {
            private readonly IMessageRepository _messageRepository;
            private readonly IReturnUtility _returnUtility;

            public CreateLanguageCommandHandler(IMessageRepository messageRepository, IReturnUtility returnUtility)
            {
                _messageRepository = messageRepository;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<Message>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
            {
                var message = await _messageRepository.GetAsync(s => s.LanguageId == request.Model.LanguageId && s.Code == request.Model.Code);
                if (message != null)
                {
                    return await _returnUtility.Fail<Message>(MessageDefinitions.KAYIT_ZATEN_MEVCUT);
                }

                _messageRepository.Add(request.Model);
                await _messageRepository.SaveChangesAsync();

                return await _returnUtility.SuccessWithData(MessageDefinitions.KAYIT_ISLEMI_BASARILI, request.Model);
            }
        }
    }
}
