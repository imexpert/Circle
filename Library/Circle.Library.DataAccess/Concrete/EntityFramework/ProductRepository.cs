using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Circle.Core.DataAccess.EntityFramework;
using Circle.Core.Entities.Concrete;
using Circle.Core.Entities.Dtos;
using Circle.Library.DataAccess.Abstract;
using Circle.Library.DataAccess.Concrete.EntityFramework.Contexts;
using Circle.Library.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Circle.Library.DataAccess.Concrete.EntityFramework
{
    public class ProductRepository : EntityRepositoryBase<Product, ProjectDbContext>, IProductRepository
    {
        public ProductRepository(ProjectDbContext context)
            : base(context)
        {
        }
    }
}