using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;

namespace Circle.Library.Business.Handlers.Departments.Queries
{
    public class GetDepartmentQuery : IRequest<ResponseMessage<Department>>
    {
        public Guid Id { get; set; }

        public class GetDepartmentQueryHandler : IRequestHandler<GetDepartmentQuery, ResponseMessage<Department>>
        {
            private readonly IDepartmentRepository _DepartmentRepository;
            private readonly IMediator _mediator;

            public GetDepartmentQueryHandler(IDepartmentRepository DepartmentRepository, IMediator mediator)
            {
                _DepartmentRepository = DepartmentRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<Department>> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
            {
                var Department = await _DepartmentRepository.GetAsync(p => p.Id == request.Id);
                if (Department == null || Department.Id == Guid.Empty)
                    return ResponseMessage<Department>.NoDataFound("Kayıt bulunamadı");

                return ResponseMessage<Department>.Success(Department);
            }
        }
    }
}