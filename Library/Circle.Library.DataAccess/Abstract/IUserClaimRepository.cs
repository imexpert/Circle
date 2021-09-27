using System.Collections.Generic;
using System.Threading.Tasks;
using Circle.Core.DataAccess;
using Circle.Core.Entities.Concrete;
using Circle.Core.Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface IUserClaimRepository : IEntityRepository<UserClaim>
    {
        Task<IEnumerable<SelectionItem>> GetUserClaimSelectedList(int userId);
        Task<IEnumerable<UserClaim>> BulkInsert(int userId, IEnumerable<UserClaim> userClaims);
    }
}