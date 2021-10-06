using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;

using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.Aspects.Autofac.Validation;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using Circle.Library.Business.Handlers.Languages.ValidationRules;
using System;
using Circle.Core.Entities.Concrete;

namespace Circle.Library.Business.Handlers.Languages.Commands
{
    public class UpdateLanguageCommand : IRequest<ResponseMessage<Language>>
    {
        public Language Model { get; set; }

        public class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand, ResponseMessage<Language>>
        {
            private readonly ILanguageRepository _languageRepository;

            public UpdateLanguageCommandHandler(ILanguageRepository languageRepository)
            {
                _languageRepository = languageRepository;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<Language>> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
            {
                var isThereLanguageRecord = await _languageRepository.GetAsync(u => u.Id == request.Model.Id);
                if (isThereLanguageRecord == null || isThereLanguageRecord.Id == Guid.Empty)
                    return ResponseMessage<Language>.NoDataFound("Kayıt bulunamadı");

                request.Model = _languageRepository.Update(request.Model);
                await _languageRepository.SaveChangesAsync();
                return ResponseMessage<Language>.Success(request.Model);
            }
        }
    }
}