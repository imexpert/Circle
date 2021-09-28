using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Circle.Core.DataAccess;
using Circle.Core.Entities.Concrete;

namespace Circle.Library.DataAccess.Abstract
{
    public interface IUserRepository : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(Guid userId);
        Task<User> GetByRefreshToken(string refreshToken);
    }
}