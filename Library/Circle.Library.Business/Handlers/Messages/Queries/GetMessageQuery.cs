using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Messages;
using Circle.Core.Utilities.Results;
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
    public class GetMessageQuery : IRequest<string>
    {
        public int MessageCode { get; set; }
        public class GetMessageQueryHandler : IRequestHandler<GetMessageQuery, string>
        {
            IMessageRepository _messageRepository;
            IHttpContextAccessor _httpContextAccessor;
            ILanguageRepository _languageRepository;

            public GetMessageQueryHandler(
                IMessageRepository messageRepository, 
                IHttpContextAccessor httpContextAccessor,
                ILanguageRepository languageRepository)
            {
                _messageRepository = messageRepository;
                _httpContextAccessor = httpContextAccessor;
                _languageRepository = languageRepository;
            }

            public async Task<string> Handle(GetMessageQuery request, CancellationToken cancellationToken)
            {
                string cultureCode = _httpContextAccessor.HttpContext.Request.Path.Value.ToString().Split('/')[2];

                var langId = await _languageRepository.GetAsync(s => s.Code == cultureCode);
                if (langId != null && langId.Id != Guid.Empty)
                {
                    var message = await _messageRepository.GetAsync(s => s.LanguageId == langId.Id && s.Code == request.MessageCode);
                    if (message != null && message.Id != Guid.Empty)
                    {
                        return message.MessageDetail;
                    }
                }

                return DefaultMessageDefinitions.DefaultMessages[request.MessageCode];
            }
        }
    }
}
