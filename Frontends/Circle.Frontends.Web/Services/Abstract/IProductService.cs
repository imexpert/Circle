using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.Entities.ComplexTypes;
using Circle.Library.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Services.Abstract
{
    public interface IProductService
    {
        Task<ResponseMessage<Product>> AddAsync(Product product);
        Task<ResponseMessage<Product>> UpdateAsync(UpdateProuctModel model);
        Task<ResponseMessage<ProductItem>> GetAsync(Guid guid);
    }
}
