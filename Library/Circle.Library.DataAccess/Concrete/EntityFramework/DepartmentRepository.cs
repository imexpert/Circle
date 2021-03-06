using Circle.Core.DataAccess.EntityFramework;
using Circle.Core.Entities.Concrete;
using Circle.Library.DataAccess.Abstract;
using Circle.Library.DataAccess.Concrete.EntityFramework.Contexts;

namespace Circle.Library.DataAccess.Concrete.EntityFramework
{
    public class DepartmentRepository : EntityRepositoryBase<Department, ProjectDbContext>, IDepartmentRepository
    {
        public DepartmentRepository(ProjectDbContext context)
              : base(context)
        {
        }
    }
}
