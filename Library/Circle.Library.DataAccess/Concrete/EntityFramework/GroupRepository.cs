using Circle.Core.DataAccess.EntityFramework;
using Circle.Core.Entities.Concrete;
using Circle.Library.DataAccess.Abstract;
using Circle.Library.DataAccess.Concrete.EntityFramework.Contexts;

namespace Circle.Library.DataAccess.Concrete.EntityFramework
{
    public class GroupRepository : EntityRepositoryBase<Group, ProjectDbContext>, IGroupRepository
    {
        public GroupRepository(ProjectDbContext context)
            : base(context)
        {
        }
    }
}