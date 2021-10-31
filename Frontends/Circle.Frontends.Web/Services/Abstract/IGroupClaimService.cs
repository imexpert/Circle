using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Services.Abstract
{
    public interface IGroupClaimService
    {
        Task<ResponseMessage<List<GroupClaim>>> GetList();
        Task<ResponseMessage<GroupClaim>> AddAsync(GroupClaim groupClaimModel);
        Task<ResponseMessage<GroupClaim>> UpdateAsync(GroupClaim groupClaimModel);
        Task<ResponseMessage<List<GroupClaim>>> GetWithIdAsync(Guid groupClaimId);
        Task<ResponseMessage<List<GroupClaim>>> DeleteAsync(Guid groupClaimId);
    }
}
