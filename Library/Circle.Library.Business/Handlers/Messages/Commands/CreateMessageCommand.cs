using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
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

            public CreateLanguageCommandHandler(IMessageRepository messageRepository)
            {
                _messageRepository = messageRepository;
            }

            public async Task<ResponseMessage<Message>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
            {
                request.Model = _messageRepository.Add(request.Model);
                await _messageRepository.SaveChangesAsync();
                return ResponseMessage<Message>.Success(request.Model);
            }
        }
    }
}
