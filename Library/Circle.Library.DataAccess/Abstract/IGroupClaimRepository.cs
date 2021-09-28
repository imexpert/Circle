using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Circle.Core.DataAccess;
using Circle.Core.Entities.Concrete;
using Circle.Core.Entities.Dtos;

namespace Circle.Library.DataAccess.Abstract
{
    public interface IGroupClaimRepository : IEntityRepository<GroupClaim>
    {
        Task<IEnumerable<SelectionItem>> GetGroupClaimsSelectedList(Guid groupId);
        Task BulkInsert(Guid groupId, IEnumerable<GroupClaim> groupClaims);
    }
}