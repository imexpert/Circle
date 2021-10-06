using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.Aspects.Autofac.Validation;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using Circle.Library.Business.Handlers.Languages.ValidationRules;

namespace Circle.Library.Business.Handlers.Languages.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CreateLanguageCommand : IRequest<ResponseMessage<Language>>
    {
        public Language Model { get; set; }

        public class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, ResponseMessage<Language>>
        {
            private readonly ILanguageRepository _languageRepository;
            private readonly IMediator _mediator;

            public CreateLanguageCommandHandler(ILanguageRepository languageRepository, IMediator mediator)
            {
                _languageRepository = languageRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<Language>> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
            {
                request.Model = _languageRepository.Add(request.Model);
                await _languageRepository.SaveChangesAsync();

                return ResponseMessage<Language>.Success(request.Model);
            }
        }
    }
}