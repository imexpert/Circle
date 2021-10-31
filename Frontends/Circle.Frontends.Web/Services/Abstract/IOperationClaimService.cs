using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Services.Abstract
{
    public interface IOperationClaimService
    {
        Task<ResponseMessage<List<OperationClaim>>> GetList();
        Task<ResponseMessage<OperationClaim>> AddAsync(OperationClaim operationClaimModel);
        Task<ResponseMessage<OperationClaim>> UpdateAsync(OperationClaim operationClaimModel);
        Task<ResponseMessage<List<OperationClaim>>> GetWithIdAsync(Guid operationClaimId);
        Task<ResponseMessage<List<OperationClaim>>> DeleteAsync(Guid operationClaimId);
    }
}
