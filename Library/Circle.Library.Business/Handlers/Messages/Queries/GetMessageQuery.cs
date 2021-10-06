using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Messages;
using Circle.Core.Utilities.Results;
using Circle.Library.Business.BusinessAspects;
using Circle.Library.Business.Helpers;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        public Guid Id { get; set; }
        public class GetMessageQueryHandler : IRequestHandler<GetMessageQuery, ResponseMessage<Message>>
        {
            private readonly IMessageRepository _messageRepository;
            private readonly IReturnUtility _returnUtility;

            public GetMessageQueryHandler(IMessageRepository messageRepository, IReturnUtility returnUtility)
            {
                _messageRepository = messageRepository;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<Message>> Handle(GetMessageQuery request, CancellationToken cancellationToken)
            {
                var group = await _messageRepository.GetAsync(x => x.Id == request.Id);

                if (group == null)
                {
                    return await _returnUtility.NoDataFound<Message>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                return _returnUtility.SuccessData(group);
            }
        }
    }
}
