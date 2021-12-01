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
    public class GetMaterialDetailsQuery : IRequest<ResponseMessage<List<CategoryAttribute>>>
    {
        public Guid MaterialId { get; set; }

        public class GetMaterialDetailsQueryHandler : IRequestHandler<GetMaterialDetailsQuery, ResponseMessage<List<CategoryAttribute>>>
        {
            private readonly ICategoryAttributeRepository _categoryAttributeRepository;
            private readonly IReturnUtility _returnUtility;

            public GetMaterialDetailsQueryHandler(
                ICategoryAttributeRepository categoryAttributeRepository, 
                IReturnUtility returnUtility)
            {
                _categoryAttributeRepository = categoryAttributeRepository;
                _returnUtility = returnUtility;
            }



            //[SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<List<CategoryAttribute>>> Handle(GetMaterialDetailsQuery request, CancellationToken cancellationToken)
            {
                var category = await _categoryAttributeRepository.GetListAsync(s=>s.LinkedAttributeId == request.MaterialId && s.TypeCode == 2);

                if (category == null)
                {
                    return await _returnUtility.NoDataFound<List<CategoryAttribute>>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                return _returnUtility.SuccessData(category);
            }
        }
    }
}
