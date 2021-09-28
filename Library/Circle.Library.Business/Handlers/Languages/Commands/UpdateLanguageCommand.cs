using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Library.Business.Constants;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.Aspects.Autofac.Validation;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using Circle.Library.Business.Handlers.Languages.ValidationRules;
using System;

namespace Circle.Library.Business.Handlers.Languages.Commands
{
    public class UpdateLanguageCommand : IRequest<IResult>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand, IResult>
        {
            private readonly ILanguageRepository _languageRepository;
            private readonly IMediator _mediator;

            public UpdateLanguageCommandHandler(ILanguageRepository languageRepository, IMediator mediator)
            {
                _languageRepository = languageRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [ValidationAspect(typeof(UpdateLanguageValidator), Priority = 2)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IResult> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
            {
                var isThereLanguageRecord = await _languageRepository.GetAsync(u => u.Id == request.Id);

                isThereLanguageRecord.Id = request.Id;
                isThereLanguageRecord.Name = request.Name;
                isThereLanguageRecord.Code = request.Code;


                _languageRepository.Update(isThereLanguageRecord);
                await _languageRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}