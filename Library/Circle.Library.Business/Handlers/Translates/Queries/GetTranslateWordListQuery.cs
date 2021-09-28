using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.Aspects.Autofac.Performance;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;

namespace Circle.Library.Business.Handlers.Translates.Queries
{
    public class GetTranslateWordListQuery : IRequest<IDataResult<Dictionary<string, string>>>
    {
        public string Lang { get; set; }

        public class GetTranslateWordListQueryHandler : IRequestHandler<GetTranslateWordListQuery,
            IDataResult<Dictionary<string, string>>>
        {
            private readonly ITranslateRepository _translateRepository;
            private readonly IMediator _mediator;

            public GetTranslateWordListQueryHandler(ITranslateRepository translateRepository, IMediator mediator)
            {
                _translateRepository = translateRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IDataResult<Dictionary<string, string>>> Handle(GetTranslateWordListQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<Dictionary<string, string>>(
                    await _translateRepository.GetTranslateWordList(request.Lang));
            }
        }
    }
}