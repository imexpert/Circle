using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.Aspects.Autofac.Performance;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Circle.Library.Business.Handlers.Languages.Queries
{
    public class GetLanguagesQuery : IRequest<IDataResult<IEnumerable<Language>>>
    {
        public class GetLanguagesQueryHandler : IRequestHandler<GetLanguagesQuery, IDataResult<IEnumerable<Language>>>
        {
            private readonly ILanguageRepository _languageRepository;
            private readonly IMediator _mediator;

            public GetLanguagesQueryHandler(ILanguageRepository languageRepository, IMediator mediator)
            {
                _languageRepository = languageRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IDataResult<IEnumerable<Language>>> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Language>>(await _languageRepository.GetListAsync());
            }
        }
    }
}