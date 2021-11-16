using Circle.Core.DataAccess;
using Circle.Core.Entities.Concrete;
using Circle.Library.Entities.Concrete;

namespace Circle.Library.DataAccess.Abstract
{
    public interface IProductRepository : IEntityRepository<Product>
    {
    }
}
