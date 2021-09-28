using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;

namespace Circle.Library.Business.Handlers.Languages.Queries
{
    public class GetLanguageQuery : IRequest<IDataResult<Language>>
    {
        public int Id { get; set; }

        public class GetLanguageQueryHandler : IRequestHandler<GetLanguageQuery, IDataResult<Language>>
        {
            private readonly ILanguageRepository _languageRepository;
            private readonly IMediator _mediator;

            public GetLanguageQueryHandler(ILanguageRepository languageRepository, IMediator mediator)
            {
                _languageRepository = languageRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IDataResult<Language>> Handle(GetLanguageQuery request, CancellationToken cancellationToken)
            {
                var language = await _languageRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Language>(language);
            }
        }
    }
}