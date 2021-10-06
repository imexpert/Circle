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
    public class GetMessageWithCodeQuery : IRequest<string>
    {
        public int MessageCode { get; set; }
        public class GetMessageWithCodeQueryHandler : IRequestHandler<GetMessageWithCodeQuery, string>
        {
            private readonly IMessageRepository _messageRepository;
            private readonly IReturnUtility _returnUtility;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly ILanguageRepository _languageRepository;

            public GetMessageWithCodeQueryHandler(
                IMessageRepository messageRepository,
                IReturnUtility returnUtility,
                IHttpContextAccessor httpContextAccessor,
                ILanguageRepository languageRepository)
            {
                _messageRepository = messageRepository;
                _returnUtility = returnUtility;
                _httpContextAccessor = httpContextAccessor;
                _languageRepository = languageRepository;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<string> Handle(GetMessageWithCodeQuery request, CancellationToken cancellationToken)
            {
                string cultureCode = _httpContextAccessor.HttpContext.Request.Path.Value.Split('/')[2];

                Language lang = await _languageRepository.GetAsync(s => s.Code == cultureCode);

                if (lang != null)
                {
                    Message message = await _messageRepository.GetAsync(s => s.LanguageId == lang.Id && s.Code == request.MessageCode);

                    if (message != null)
                    {
                        return message.MessageDetail;
                    }
                }

                return DefaultMessageDefinitions.DefaultMessages[request.MessageCode];
            }
        }
    }
}
