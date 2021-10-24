using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Services.Abstract
{
    public interface IUserService
    {
        Task<ResponseMessage<List<User>>> GetList();
    }
}
