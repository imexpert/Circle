﻿using System.Threading;
using System.Threading.Tasks;
using Circle.Library.Business.BusinessAspects;
using Circle.Library.Business.Constants;
using Circle.Core.Aspects.Autofac.Caching;
using Circle.Core.Aspects.Autofac.Logging;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Circle.Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Circle.Library.Business.Handlers.OperationClaims.Commands
{
    public class UpdateOperationClaimCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }

        public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, IResult>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;

            public UpdateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository)
            {
                _operationClaimRepository = operationClaimRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            public async Task<IResult> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                var isOperationClaimExists = await _operationClaimRepository.GetAsync(u => u.Id == request.Id);
                isOperationClaimExists.Alias = request.Alias;
                isOperationClaimExists.Description = request.Description;

                _operationClaimRepository.Update(isOperationClaimExists);
                await _operationClaimRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}