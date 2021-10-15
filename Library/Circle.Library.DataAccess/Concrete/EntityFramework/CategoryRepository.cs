using Circle.Core.DataAccess.EntityFramework;
using Circle.Library.DataAccess.Abstract;
using Circle.Library.DataAccess.Concrete.EntityFramework.Contexts;
using Circle.Library.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
