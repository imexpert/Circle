using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using Circle.Library.Entities.ComplexTypes;
using Circle.Library.Business.Helpers;
using Circle.Core.Utilities.Messages;
using System;

namespace Circle.Library.Business.Handlers.Departments.Commands
{
    public class CreateDepartmentCommand : IRequest<ResponseMessage<CreateDepartmentModel>>
    {
        public CreateDepartmentModel Model { get; set; }

        public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, ResponseMessage<CreateDepartmentModel>>
        {
            private readonly IDepartmentRepository _DepartmentRepository;
            private readonly IMediator _mediator;
            private readonly IReturnUtility _returnUtility;

            public CreateDepartmentCommandHandler(
                IDepartmentRepository DepartmentRepository, 
                IMediator mediator,
                IReturnUtility returnUtility)
            {
                _DepartmentRepository = DepartmentRepository;
                _mediator = mediator;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<CreateDepartmentModel>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
            {
                Guid g = Guid.NewGuid();

                Department dTr = new Department()
                {
                    LanguageId = LanguageExtension.TrLanguageId,
                    Status = request.Model.Status == 1,
                    Title = request.Model.TitleTr,
                    Id = g
                };

                Department dUs = new Department()
                {
                    LanguageId = LanguageExtension.UsLanguageId,
                    Status = request.Model.Status == 1,
                    Title = request.Model.TitleUs,
                    Id = g
                };

                var us = await _DepartmentRepository.GetAsync(s => s.LanguageId == LanguageExtension.UsLanguageId && s.Title == dUs.Title);
                var tr = await _DepartmentRepository.GetAsync(s => s.LanguageId == LanguageExtension.TrLanguageId && s.Title == dTr.Title);

                if (us != null || tr != null)
                {
                    return await _returnUtility.Fail<CreateDepartmentModel>(MessageDefinitions.KAYIT_ZATEN_MEVCUT);
                }

                _DepartmentRepository.Add(dTr);
                _DepartmentRepository.Add(dUs);
                await _DepartmentRepository.SaveChangesAsync();

                return await _returnUtility.SuccessWithData(MessageDefinitions.KAYIT_ISLEMI_BASARILI, request.Model);
            }
        }
    }
}