using Circle.Core.DataAccess.EntityFramework;
using Circle.Core.Entities.Concrete;
using Circle.Library.DataAccess.Abstract;
using Circle.Library.DataAccess.Concrete.EntityFramework.Contexts;

namespace Circle.Library.DataAccess.Concrete.EntityFramework
{
    public class CategoryAttributeRepository : EntityRepositoryBase<CategoryAttribute, ProjectDbContext>, ICategoryAttributeRepository
    {
        public CategoryAttributeRepository(ProjectDbContext context)
              : base(context)
        {
        }
    }
}
