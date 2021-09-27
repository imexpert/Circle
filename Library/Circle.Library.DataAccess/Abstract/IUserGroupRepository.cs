using System.Collections.Generic;
using System.Threading.Tasks;
using Circle.Core.DataAccess;
using Circle.Core.Entities.Concrete;
using Circle.Core.Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface IUserGroupRepository : IEntityRepository<UserGroup>
    {
        Task<IEnumerable<SelectionItem>> GetUserGroupSelectedList(int userId);
        Task<IEnumerable<SelectionItem>> GetUsersInGroupSelectedListByGroupId(int groupId);
        Task BulkInsert(int userId, IEnumerable<UserGroup> userGroups);
        Task BulkInsertByGroupId(int groupId, IEnumerable<UserGroup> userGroups);
    }
}