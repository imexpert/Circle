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
using Circle.Library.DataAccess.Abstract;
using MediatR;

namespace Circle.Library.Business.Handlers.Translates.Queries
{
    public class GetTranslatesQuery : IRequest<IDataResult<IEnumerable<Translate>>>
    {
        public class
            GetTranslatesQueryHandler : IRequestHandler<GetTranslatesQuery, IDataResult<IEnumerable<Translate>>>
        {
            private readonly ITranslateRepository _translateRepository;
            private readonly IMediator _mediator;

            public GetTranslatesQueryHandler(ITranslateRepository translateRepository, IMediator mediator)
            {
                _translateRepository = translateRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IDataResult<IEnumerable<Translate>>> Handle(GetTranslatesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Translate>>(await _translateRepository.GetListAsync());
            }
        }
    }
}