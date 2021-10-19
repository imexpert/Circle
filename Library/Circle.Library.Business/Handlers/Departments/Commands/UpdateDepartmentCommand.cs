using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;

using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.Aspects.Autofac.Validation;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;
using Circle.Library.Entities.Concrete;
using Circle.Core.Entities.Concrete;

namespace Circle.Library.Business.Handlers.Departments.Commands
{
    public class UpdateDepartmentCommand : IRequest<ResponseMessage<Department>>
    {
        public Department Model { get; set; }

        public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, ResponseMessage<Department>>
        {
            private readonly IDepartmentRepository _DepartmentRepository;

            public UpdateDepartmentCommandHandler(IDepartmentRepository DepartmentRepository)
            {
                _DepartmentRepository = DepartmentRepository;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<Department>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
            {
                var isThereDepartmentRecord = await _DepartmentRepository.GetAsync(u => u.Id == request.Model.Id);
                if (isThereDepartmentRecord == null || isThereDepartmentRecord.Id == Guid.Empty)
                    return ResponseMessage<Department>.NoDataFound("Kayıt bulunamadı");

                request.Model = _DepartmentRepository.Update(request.Model);
                await _DepartmentRepository.SaveChangesAsync();
                return ResponseMessage<Department>.Success(request.Model);
            }
        }
    }
}