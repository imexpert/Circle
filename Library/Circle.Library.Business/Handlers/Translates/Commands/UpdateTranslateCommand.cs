using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Library.Business.Constants;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.Aspects.Autofac.Validation;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using Circle.Library.Business.Handlers.Translates.ValidationRules;

namespace Circle.Library.Business.Handlers.Translates.Commands
{
    public class UpdateTranslateCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int LangId { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }

        public class UpdateTranslateCommandHandler : IRequestHandler<UpdateTranslateCommand, IResult>
        {
            private readonly ITranslateRepository _translateRepository;
            private readonly IMediator _mediator;

            public UpdateTranslateCommandHandler(ITranslateRepository translateRepository, IMediator mediator)
            {
                _translateRepository = translateRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [ValidationAspect(typeof(CreateTranslateValidator), Priority = 2)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IResult> Handle(UpdateTranslateCommand request, CancellationToken cancellationToken)
            {
                var isThereTranslateRecord = await _translateRepository.GetAsync(u => u.Id == request.Id);

                isThereTranslateRecord.Id = request.Id;
                isThereTranslateRecord.LangId = request.LangId;
                isThereTranslateRecord.Value = request.Value;
                isThereTranslateRecord.Code = request.Code;


                _translateRepository.Update(isThereTranslateRecord);
                await _translateRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}