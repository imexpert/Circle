using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Library.Business.Constants;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.Aspects.Autofac.Validation;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using Circle.Library.Business.Handlers.Translates.ValidationRules;

namespace Circle.Library.Business.Handlers.Translates.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CreateTranslateCommand : IRequest<IResult>
    {
        public int LangId { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }


        public class CreateTranslateCommandHandler : IRequestHandler<CreateTranslateCommand, IResult>
        {
            private readonly ITranslateRepository _translateRepository;
            private readonly IMediator _mediator;

            public CreateTranslateCommandHandler(ITranslateRepository translateRepository, IMediator mediator)
            {
                _translateRepository = translateRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [ValidationAspect(typeof(CreateTranslateValidator), Priority = 2)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IResult> Handle(CreateTranslateCommand request, CancellationToken cancellationToken)
            {
                var isThereTranslateRecord = _translateRepository.Query()
                    .Any(u => u.LangId == request.LangId && u.Code == request.Code);

                if (isThereTranslateRecord == true)
                {
                    return new ErrorResult(Messages.NameAlreadyExist);
                }

                var addedTranslate = new Translate
                {
                    LangId = request.LangId,
                    Value = request.Value,
                    Code = request.Code,
                };

                _translateRepository.Add(addedTranslate);
                await _translateRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}