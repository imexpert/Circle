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
using Circle.Library.Business.Helpers;
using Circle.Core.Utilities.Messages;

namespace Circle.Library.Business.Handlers.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest<ResponseMessage<Category>>
    {
        public Category Model { get; set; }

        public class UpdateGroupCommandHandler : IRequestHandler<UpdateCategoryCommand, ResponseMessage<Category>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IReturnUtility _returnUtility;

            public UpdateGroupCommandHandler(ICategoryRepository categoryRepository, IReturnUtility returnUtility)
            {
                _categoryRepository = categoryRepository;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<Category>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = await _categoryRepository.GetAsync(s => s.Id == request.Model.Id);
                if (category == null)
                {
                    return await _returnUtility.NoDataFound<Category>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                category.Name = request.Model.Name;

                _categoryRepository.Update(category);
                await _categoryRepository.SaveChangesAsync();

                return await _returnUtility.SuccessWithData(MessageDefinitions.GUNCELLEME_ISLEMI_BASARILI, category);
            }
        }
    }
}