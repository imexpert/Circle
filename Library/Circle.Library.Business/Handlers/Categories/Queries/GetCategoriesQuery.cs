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
using System.Collections.Generic;

namespace Circle.Library.Business.Handlers.Categories.Queries
{
    public class GetCategoriesQuery : IRequest<ResponseMessage<List<Category>>>
    {
        public Guid Id { get; set; }

        public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, ResponseMessage<List<Category>>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IReturnUtility _returnUtility;

            public GetCategoriesQueryHandler(
                ICategoryRepository categoryRepository,
                IReturnUtility returnUtility)
            {
                _categoryRepository = categoryRepository;
                _returnUtility = returnUtility;
            }

            //[SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<List<Category>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
            {
                var category = await _categoryRepository.GetListAsync();

                if (category == null)
                {
                    return await _returnUtility.NoDataFound<List<Category>>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                return _returnUtility.SuccessData(category);
            }
        }
    }
}