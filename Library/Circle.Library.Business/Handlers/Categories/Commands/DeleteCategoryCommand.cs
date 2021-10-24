using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;

namespace Circle.Library.Business.Handlers.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest<ResponseMessage<NoContent>>
    {
        public Guid Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ResponseMessage<NoContent>>
        {
            private readonly ICategoryRepository _categoryRepository;

            public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
            {
                _categoryRepository = categoryRepository;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<NoContent>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var languageToDelete = _categoryRepository.Get(p => p.Id == request.Id);

                if (languageToDelete == null || languageToDelete.Id == Guid.Empty)
                    return ResponseMessage<NoContent>.NoDataFound("Kategori tanımı bulunamadı");

                _categoryRepository.Delete(languageToDelete);
                await _categoryRepository.SaveChangesAsync();
                return ResponseMessage<NoContent>.Success();
            }
        }
    }
}