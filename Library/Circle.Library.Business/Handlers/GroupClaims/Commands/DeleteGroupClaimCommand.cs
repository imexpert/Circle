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
using Circle.Core.Utilities.Messages;
using Circle.Library.Business.Helpers;

namespace Circle.Library.Business.Handlers.GroupClaims.Commands
{
    public class DeleteGroupClaimCommand : IRequest<ResponseMessage<NoContent>>
    {
        public Guid Id { get; set; }

        public class DeleteGroupClaimCommandHandler : IRequestHandler<DeleteGroupClaimCommand, ResponseMessage<NoContent>>
        {
            private readonly IGroupClaimRepository _groupClaimRepository;
            private readonly IReturnUtility _returnUtility;

            public DeleteGroupClaimCommandHandler(IGroupClaimRepository groupClaimRepository, IReturnUtility returnUtility)
            {
                _groupClaimRepository = groupClaimRepository;
                _returnUtility = returnUtility;
            }

            public async Task<ResponseMessage<NoContent>> Handle(DeleteGroupClaimCommand request, CancellationToken cancellationToken)
            {
                var groupClaimToDelete = await _groupClaimRepository.GetAsync(x => x.Id == request.Id);
                if (groupClaimToDelete == null || groupClaimToDelete.Id == Guid.Empty)
                {
                    return await _returnUtility.NoDataFound<NoContent>(MessageDefinitions.KAYIT_BULUNAMADI);
                }

                _groupClaimRepository.Delete(groupClaimToDelete);
                await _groupClaimRepository.SaveChangesAsync();

                return await _returnUtility.Success<NoContent>(MessageDefinitions.SILME_ISLEMI_BASARILI);
            }
        }
    }
}