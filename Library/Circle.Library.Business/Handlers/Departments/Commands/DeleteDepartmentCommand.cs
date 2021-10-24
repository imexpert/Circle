using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;

namespace Circle.Library.Business.Handlers.Departments.Commands
{
    public class DeleteDepartmentCommand : IRequest<ResponseMessage<NoContent>>
    {
        public Guid Id { get; set; }

        public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, ResponseMessage<NoContent>>
        {
            private readonly IDepartmentRepository _DepartmentRepository;

            public DeleteDepartmentCommandHandler(IDepartmentRepository DepartmentRepository)
            {
                _DepartmentRepository = DepartmentRepository;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<NoContent>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
            {
                var languageToDelete = _DepartmentRepository.Get(p => p.Id == request.Id);

                if (languageToDelete == null || languageToDelete.Id == Guid.Empty)
                    return ResponseMessage<NoContent>.NoDataFound("Kategori tanımı bulunamadı");

                _DepartmentRepository.Delete(languageToDelete);
                await _DepartmentRepository.SaveChangesAsync();
                return ResponseMessage<NoContent>.Success();
            }
        }
    }
}