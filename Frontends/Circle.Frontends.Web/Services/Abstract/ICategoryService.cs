using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Services.Abstract
{
    public interface ICategoryService
    {
        Task<ResponseMessage<CategoryListModel>> GetAllSubCategories(Guid categoryId);
    }
}
