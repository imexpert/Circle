using Circle.Core.DataAccess.EntityFramework;
using Circle.Core.Entities.Concrete;
using Circle.Library.DataAccess.Abstract;
using Circle.Library.DataAccess.Concrete.EntityFramework.Contexts;

namespace Circle.Library.DataAccess.Concrete.EntityFramework
{
    public class CategoryRepository : EntityRepositoryBase<Category, ProjectDbContext>, ICategoryRepository
    {
        public CategoryRepository(ProjectDbContext context)
              : base(context)
        {
        }
    }
}
