using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Library.Business.Constants;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;

namespace Circle.Library.Business.Handlers.Languages.Commands
{
    public class DeleteLanguageCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteLanguageCommandHandler : IRequestHandler<DeleteLanguageCommand, IResult>
        {
            private readonly ILanguageRepository _languageRepository;
            private readonly IMediator _mediator;

            public DeleteLanguageCommandHandler(ILanguageRepository languageRepository, IMediator mediator)
            {
                _languageRepository = languageRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
            {
                var languageToDelete = _languageRepository.Get(p => p.Id == request.Id);

                _languageRepository.Delete(languageToDelete);
                await _languageRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}