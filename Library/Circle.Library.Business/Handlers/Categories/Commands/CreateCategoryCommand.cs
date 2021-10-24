using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;

namespace Circle.Library.Business.Handlers.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<ResponseMessage<Category>>
    {
        public Category Model { get; set; }

        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ResponseMessage<Category>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMediator _mediator;

            public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMediator mediator)
            {
                _categoryRepository = categoryRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<Category>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                request.Model = _categoryRepository.Add(request.Model);
                await _categoryRepository.SaveChangesAsync();

                return ResponseMessage<Category>.Success(request.Model);
            }
        }
    }
}