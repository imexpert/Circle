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

namespace Circle.Library.Business.Handlers.Departments.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CreateDepartmentCommand : IRequest<ResponseMessage<Department>>
    {
        public Department Model { get; set; }

        public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, ResponseMessage<Department>>
        {
            private readonly IDepartmentRepository _DepartmentRepository;
            private readonly IMediator _mediator;

            public CreateDepartmentCommandHandler(IDepartmentRepository DepartmentRepository, IMediator mediator)
            {
                _DepartmentRepository = DepartmentRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<Department>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
            {
                request.Model = _DepartmentRepository.Add(request.Model);
                await _DepartmentRepository.SaveChangesAsync();

                return ResponseMessage<Department>.Success(request.Model);
            }
        }
    }
}