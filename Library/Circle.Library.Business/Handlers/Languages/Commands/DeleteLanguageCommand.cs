using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;

namespace Circle.Library.Business.Handlers.Languages.Commands
{
    public class DeleteLanguageCommand : IRequest<ResponseMessage<NoContent>>
    {
        public Guid Id { get; set; }

        public class DeleteLanguageCommandHandler : IRequestHandler<DeleteLanguageCommand, ResponseMessage<NoContent>>
        {
            private readonly ILanguageRepository _languageRepository;

            public DeleteLanguageCommandHandler(ILanguageRepository languageRepository)
            {
                _languageRepository = languageRepository;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<NoContent>> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
            {
                var languageToDelete = _languageRepository.Get(p => p.Id == request.Id);

                if (languageToDelete == null || languageToDelete.Id == Guid.Empty)
                    return ResponseMessage<NoContent>.NoDataFound("Dil tanımı bulunamadı");

                _languageRepository.Delete(languageToDelete);
                await _languageRepository.SaveChangesAsync();
                return ResponseMessage<NoContent>.Success();
            }
        }
    }
}