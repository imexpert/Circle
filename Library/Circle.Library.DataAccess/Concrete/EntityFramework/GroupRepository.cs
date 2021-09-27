using Circle.Core.DataAccess.EntityFramework;
using Circle.Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class GroupRepository : EntityRepositoryBase<Group, ProjectDbContext>, IGroupRepository
    {
        public GroupRepository(ProjectDbContext context)
            : base(context)
        {
        }
    }
}