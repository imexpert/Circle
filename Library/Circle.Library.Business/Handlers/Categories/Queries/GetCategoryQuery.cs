using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;

namespace Circle.Library.Business.Handlers.Categories.Queries
{
    public class GetCategoryQuery : IRequest<ResponseMessage<Category>>
    {
        public Guid Id { get; set; }

        public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, ResponseMessage<Category>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMediator _mediator;

            public GetCategoryQueryHandler(ICategoryRepository categoryRepository, IMediator mediator)
            {
                _categoryRepository = categoryRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<Category>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
            {
                var category = await _categoryRepository.GetAsync(p => p.Id == request.Id);
                if (category == null || category.Id == Guid.Empty)
                    return ResponseMessage<Category>.NoDataFound("Kayıt bulunamadı");

                return ResponseMessage<Category>.Success(category);
            }
        }
    }
}