using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Messages;
using Circle.Core.Utilities.Results;
using Circle.Library.Business.Helpers;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Circle.Library.Business.Handlers.CategoryAttributes.Queries
{
    public class GetCategoryAttributesQuery : IRequest<ResponseMessage<List<CategoryAttribute>>>
    {
        public Guid CategoryId { get; set; }

        public class GetCategoryAttributesQueryHandler : IRequestHandler<GetCategoryAttributesQuery, ResponseMessage<List<CategoryAttribute>>>
        {
            private readonly ICategoryAttributeRepository _categoryAttributeRepository;
            private readonly IReturnUtility _returnUtility;

            public GetCategoryAttributesQueryHandler(ICategoryAttributeRepository categoryAttributeRepository, IReturnUtility returnUtility)
            {
                _categoryAttributeRepository = categoryAttributeRepository;
                _returnUtility = returnUtility;
            }



            //[SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<List<CategoryAttribute>>> Handle(GetCategoryAttributesQuery request, CancellationToken cancellationToken)
            {
                var category = await _categoryAttributeRepository.GetListAsync(s=>s.CategoryId == request.CategoryId);

                if (category == null)
                {
                    return await _returnUtility.NoDataFound<List<CategoryAttribute>>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                return _returnUtility.SuccessData(category);
            }
        }
    }
}
