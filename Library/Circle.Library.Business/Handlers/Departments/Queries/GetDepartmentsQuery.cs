using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;

using System.Collections.Generic;
using Circle.Library.Business.Helpers;
using Circle.Core.Utilities.Messages;
using System.Linq;

namespace Circle.Library.Business.Handlers.Departments.Queries
{
    public class GetDepartmentsQuery : IRequest<ResponseMessage<List<Department>>>
    {
        public Guid Id { get; set; }

        public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, ResponseMessage<List<Department>>>
        {
            private readonly IDepartmentRepository _DepartmentRepository;
            private readonly IMediator _mediator;
            private readonly IReturnUtility _returnUtility;

            public GetDepartmentsQueryHandler(IDepartmentRepository DepartmentRepository, IMediator mediator, IReturnUtility returnUtility)
            {
                _DepartmentRepository = DepartmentRepository;
                _mediator = mediator;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<List<Department>>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
            {
                var list = await _DepartmentRepository.GetListAsync(s=>s.LanguageId == LanguageExtension.LanguageId);
                if (list == null || list.Count() <= 0)
                {
                    return await _returnUtility.NoDataFound<List<Department>>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                return _returnUtility.SuccessData(list);
            }
        }
    }
}