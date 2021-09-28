using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Circle.Core.DataAccess;
using Circle.Core.Entities.Concrete;
using Circle.Core.Entities.Dtos;

namespace Circle.Library.DataAccess.Abstract
{
    public interface IUserClaimRepository : IEntityRepository<UserClaim>
    {
        Task<IEnumerable<SelectionItem>> GetUserClaimSelectedList(Guid userId);
        Task<IEnumerable<UserClaim>> BulkInsert(Guid userId, IEnumerable<UserClaim> userClaims);
    }
}