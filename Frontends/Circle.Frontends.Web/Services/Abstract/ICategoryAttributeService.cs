using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Services.Abstract
{
    public interface ICategoryAttributeService
    {
        Task<ResponseMessage<List<CategoryAttribute>>> GetAllAttributes(Guid categoryId);
        Task<ResponseMessage<List<CategoryAttribute>>> GetMaterials(Guid productId);
        Task<ResponseMessage<List<CategoryAttribute>>> GetMaterialDetails(Guid materialId);
        Task<ResponseMessage<List<CategoryAttribute>>> GetDiameters(Guid productId);
        Task<ResponseMessage<List<CategoryAttribute>>> GetLengths(Guid productId);
    }
}
