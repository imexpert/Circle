using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;
using Circle.Core.Entities.Concrete;

namespace Circle.Library.Business.Handlers.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest<ResponseMessage<Category>>
    {
        public Category Model { get; set; }

        public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ResponseMessage<Category>>
        {
            private readonly ICategoryRepository _categoryRepository;

            public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
            {
                _categoryRepository = categoryRepository;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<Category>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                var isThereCategoryRecord = await _categoryRepository.GetAsync(u => u.Id == request.Model.Id);
                if (isThereCategoryRecord == null || isThereCategoryRecord.Id == Guid.Empty)
                    return ResponseMessage<Category>.NoDataFound("Kayıt bulunamadı");

                request.Model = _categoryRepository.Update(request.Model);
                await _categoryRepository.SaveChangesAsync();
                return ResponseMessage<Category>.Success(request.Model);
            }
        }
    }
}