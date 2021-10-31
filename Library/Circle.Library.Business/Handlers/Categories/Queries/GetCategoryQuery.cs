using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;
using Circle.Library.Business.Helpers;
using Circle.Core.Utilities.Messages;

namespace Circle.Library.Business.Handlers.Categories.Queries
{
    public class GetCategoryQuery : IRequest<ResponseMessage<Category>>
    {
        public Guid Id { get; set; }

        public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, ResponseMessage<Category>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IReturnUtility _returnUtility;

            public GetCategoryQueryHandler(
                ICategoryRepository categoryRepository,
                IReturnUtility returnUtility)
            {
                _categoryRepository = categoryRepository;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<Category>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
            {
                var category = await _categoryRepository.GetAsync(x => x.Id == request.Id);

                if (category == null)
                {
                    return await _returnUtility.NoDataFound<Category>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                return _returnUtility.SuccessData(category);
            }
        }
    }
}