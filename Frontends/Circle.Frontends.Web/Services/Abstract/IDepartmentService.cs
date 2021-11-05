using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.Entities.ComplexTypes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Services.Abstract
{
    public interface IDepartmentService
    {
        Task<ResponseMessage<List<Department>>> GetList();
        Task<ResponseMessage<CreateDepartmentModel>> AddAsync(CreateDepartmentModel model);
    }
}
