using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Messages;
using Circle.Core.Utilities.Results;
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
    public class GetMessagesQuery : IRequest<ResponseMessage<List<Message>>>
    {
        public int MessageCode { get; set; }
        public class GetMessageQueryHandler : IRequestHandler<GetMessagesQuery, ResponseMessage<List<Message>>>
        {
            private readonly IMessageRepository _messageRepository;
            private readonly IReturnUtility _returnUtility;

            public GetMessageQueryHandler(IMessageRepository messageRepository, IReturnUtility returnUtility)
            {
                _messageRepository = messageRepository;
                _returnUtility = returnUtility;
            }

            public async Task<ResponseMessage<List<Message>>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
            {
                var list = await _messageRepository.GetListAsync();
                if (list == null || list.Count() <= 0)
                {
                    return await _returnUtility.NoDataFound<List<Message>>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                return _returnUtility.SuccessData(list);
            }
        }
    }
}
