using Circle.Core.Utilities.Results;
using Circle.Core.Utilities.Security.Jwt;
using Circle.Library.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Services.Abstract
{
    public interface IAuthService
    {
        Task<ResponseMessage<AccessToken>> LoginAsync(LoginModel loginModel);
    }
}
