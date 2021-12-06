using Circle.Core.Entities.Concrete;
using Circle.Core.Entities.Enums;
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
    public class GetLengthsQuery : IRequest<ResponseMessage<List<CategoryAttribute>>>
    {
        public Guid ProductId { get; set; }

        public class GetLengthsQueryHandler : IRequestHandler<GetLengthsQuery, ResponseMessage<List<CategoryAttribute>>>
        {
            private readonly ICategoryAttributeRepository _categoryAttributeRepository;
            private readonly IReturnUtility _returnUtility;
            private readonly IProductRepository _productRepository;

            public GetLengthsQueryHandler(
                ICategoryAttributeRepository categoryAttributeRepository,
                IReturnUtility returnUtility,
                IProductRepository productRepository)
            {
                _categoryAttributeRepository = categoryAttributeRepository;
                _returnUtility = returnUtility;
                _productRepository = productRepository;
            }



            //[SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<List<CategoryAttribute>>> Handle(GetLengthsQuery request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetAsync(s => s.Id == request.ProductId);

                var category = await _categoryAttributeRepository.GetListAsync(s => s.CategoryId == product.CategoryId && s.TypeCode == ((int)MaterialTypes.Length));

                if (category == null)
                {
                    return await _returnUtility.NoDataFound<List<CategoryAttribute>>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                category = category.OrderBy(s => s.Order).ToList();
                return _returnUtility.SuccessData(category);
            }
        }
    }
}
