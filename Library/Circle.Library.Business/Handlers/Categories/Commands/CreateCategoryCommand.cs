using Circle.Core.Utilities.Messages;
using Circle.Core.Utilities.Results;
using Circle.Library.Business.BusinessAspects;
using Circle.Library.Business.Helpers;
using Circle.Library.DataAccess.Abstract;
using Circle.Library.Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Circle.Library.Business.Handlers.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<ResponseMessage<Category>>
    {
        public Category Model { get; set; }

        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ResponseMessage<Category>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IReturnUtility _returnUtility;

            public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IReturnUtility returnUtility)
            {
                _categoryRepository = categoryRepository;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<Category>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = await _categoryRepository.GetAsync(s => s.Name == request.Model.Name.Trim().ToUpper());
                if (category != null)
                {
                    return await _returnUtility.Fail<Category>(MessageDefinitions.KAYIT_ZATEN_MEVCUT);
                }

                category = new Category
                {
                    Name = request.Model.Name
                };
                _categoryRepository.Add(category);
                await _categoryRepository.SaveChangesAsync();

                return await _returnUtility.SuccessWithData(MessageDefinitions.KAYIT_ISLEMI_BASARILI, category);
            }
        }
    }
}