using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Circle.Library.Business.Handlers.Messages.Queries
{
    public class GetMessageQuery : IRequest<ResponseMessage<Message>>
    {
        public string LanguageCode { get; set; }
        public class GetMessageQueryHandler : IRequestHandler<GetMessageQuery, ResponseMessage<Message>>
        {
            IMessageRepository _messageRepository;

            public GetMessageQueryHandler(IMessageRepository messageRepository)
            {
                _messageRepository = messageRepository;
            }

            public Task<ResponseMessage<Message>> Handle(GetMessageQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
