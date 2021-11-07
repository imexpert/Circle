using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;

namespace Circle.Library.Business.Handlers.Languages.Queries
{
    public class GetLanguageByCodeQuery : IRequest<ResponseMessage<Language>>
    {
        public string Code { get; set; }

        public class GetLanguageByCodeQueryHandler : IRequestHandler<GetLanguageByCodeQuery, ResponseMessage<Language>>
        {
            private readonly ILanguageRepository _languageRepository;
            private readonly IMediator _mediator;

            public GetLanguageByCodeQueryHandler(ILanguageRepository languageRepository, IMediator mediator)
            {
                _languageRepository = languageRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<Language>> Handle(GetLanguageByCodeQuery request, CancellationToken cancellationToken)
            {
                var language = await _languageRepository.GetAsync(p => p.Code == request.Code);
                if (language == null || language.Id == Guid.Empty)
                    return ResponseMessage<Language>.NoDataFound("Kayıt bulunamadı");

                return ResponseMessage<Language>.Success(language);
            }
        }
    }
}