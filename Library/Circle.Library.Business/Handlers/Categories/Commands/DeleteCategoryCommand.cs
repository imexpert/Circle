using Circle.Core.Utilities.Messages;
using Circle.Core.Utilities.Results;
using Circle.Library.Business.BusinessAspects;
using Circle.Library.Business.Helpers;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Circle.Library.Business.Handlers.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest<ResponseMessage<NoContent>>
    {
        public Guid Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ResponseMessage<NoContent>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IReturnUtility _returnUtility;

            public DeleteCategoryCommandHandler(
                ICategoryRepository categoryRepository,
                IReturnUtility returnUtility)
            {
                _categoryRepository = categoryRepository;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<NoContent>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var categoryToDelete = await _categoryRepository.GetAsync(x => x.Id == request.Id);

                if (categoryToDelete == null || categoryToDelete.Id == Guid.Empty)
                {
                    return await _returnUtility.NoDataFound<NoContent>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                _categoryRepository.Delete(categoryToDelete);
                await _categoryRepository.SaveChangesAsync();

                return await _returnUtility.Success<NoContent>(MessageDefinitions.SILME_ISLEMI_BASARILI);
            }
        }
    }
}