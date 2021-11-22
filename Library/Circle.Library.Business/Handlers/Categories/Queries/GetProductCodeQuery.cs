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
using System.Linq;

namespace Circle.Library.Business.Handlers.Categories.Queries
{
    public class GetProductCodeQuery : IRequest<List<Category>>
    {
        public Guid Id { get; set; }

        public class GetProductCodeQueryHandler : IRequestHandler<GetProductCodeQuery, List<Category>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IReturnUtility _returnUtility;

            public GetProductCodeQueryHandler(
                ICategoryRepository categoryRepository,
                IReturnUtility returnUtility)
            {
                _categoryRepository = categoryRepository;
                _returnUtility = returnUtility;
            }

            //[SecuredOperation(Priority = 1)]
            public async Task<List<Category>> Handle(GetProductCodeQuery request, CancellationToken cancellationToken)
            {
                List<Category> urunKodList = new List<Category>();

                var categoryList = await _categoryRepository.GetListAsync();

                urunKodList = GenerateProductCode(categoryList, request.Id, urunKodList);

                return urunKodList;
            }

            private List<Category> GenerateProductCode(List<Category> all, Guid guid, List<Category> urunKodList)
            {
                if (guid == Guid.Empty)
                    return urunKodList;

                var current = all.FirstOrDefault(s => s.Id == guid);

                if (!string.IsNullOrEmpty(current.Code))
                    urunKodList.Add(new Category()
                    {
                        Code = current.Code,
                        Name = current.Name
                    });

                if (current.LinkedCategoryId.HasValue)
                {
                    GenerateProductCode(all, current.LinkedCategoryId.Value, urunKodList);
                }

                return urunKodList;
            }
        }
    }
}