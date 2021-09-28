using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Circle.Core.DataAccess;
using Circle.Core.Entities.Concrete;
using Circle.Core.Entities.Dtos;

namespace Circle.Library.DataAccess.Abstract
{
    public interface IUserGroupRepository : IEntityRepository<UserGroup>
    {
        Task<IEnumerable<SelectionItem>> GetUserGroupSelectedList(Guid userId);
        Task<IEnumerable<SelectionItem>> GetUsersInGroupSelectedListByGroupId(Guid groupId);
        Task BulkInsert(Guid userId, IEnumerable<UserGroup> userGroups);
        Task BulkInsertByGroupId(Guid groupId, IEnumerable<UserGroup> userGroups);
    }
}