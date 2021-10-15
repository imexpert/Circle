using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.Aspects.Autofac.Validation;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using Circle.Library.Business.Handlers.Languages.ValidationRules;
using Circle.Library.Entities.Concrete;

namespace Circle.Library.Business.Handlers.Categories.Commands
{
    /// <summary>
    ///
    /// </summary>
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