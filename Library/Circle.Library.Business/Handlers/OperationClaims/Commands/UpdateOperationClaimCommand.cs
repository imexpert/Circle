using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;

using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Utilities.Results;
using Circle.Library.DataAccess.Abstract;
using MediatR;
using System;
using Circle.Core.Entities.Concrete;
using Circle.Library.Business.Helpers;
using Circle.Core.Utilities.Messages;

namespace Circle.Library.Business.Handlers.OperationClaims.Commands
{
    public class UpdateOperationClaimCommand : IRequest<ResponseMessage<OperationClaim>>
    {
        public OperationClaim Model { get; set; }

        public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, ResponseMessage<OperationClaim>>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IReturnUtility _returnUtility;

            public UpdateOperationClaimCommandHandler(
                IOperationClaimRepository operationClaimRepository, 
                IReturnUtility returnUtility)
            {
                _operationClaimRepository = operationClaimRepository;
                _returnUtility = returnUtility;
            }

            [SecuredOperation(Priority = 1)]
            public async Task<ResponseMessage<OperationClaim>> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                var isOperationClaimExists = await _operationClaimRepository.GetAsync(u => u.Id == request.Model.Id);
                if (isOperationClaimExists == null)
                {
                    return await _returnUtility.NoDataFound<OperationClaim>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                isOperationClaimExists.Name = request.Model.Name;
                isOperationClaimExists.Alias = request.Model.Alias;
                isOperationClaimExists.Description = request.Model.Description;

                _operationClaimRepository.Update(isOperationClaimExists);
                await _operationClaimRepository.SaveChangesAsync();

                return await _returnUtility.SuccessWithData(MessageDefinitions.GUNCELLEME_ISLEMI_BASARILI, isOperationClaimExists);
            }
        }
    }
}