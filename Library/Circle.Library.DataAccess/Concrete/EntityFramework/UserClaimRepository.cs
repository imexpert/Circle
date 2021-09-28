using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Circle.Core.DataAccess.EntityFramework;
using Circle.Core.Entities.Concrete;
using Circle.Core.Entities.Dtos;
using Circle.Library.DataAccess.Abstract;
using Circle.Library.DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Circle.Library.DataAccess.Concrete.EntityFramework
{
    public class UserClaimRepository : EntityRepositoryBase<UserClaim, ProjectDbContext>, IUserClaimRepository
    {
        public UserClaimRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<UserClaim>> BulkInsert(Guid userId, IEnumerable<UserClaim> userClaims)
        {
            var DbClaimList = Context.UserClaims.Where(x => x.UserId == userId);

            Context.UserClaims.RemoveRange(DbClaimList);
            await Context.UserClaims.AddRangeAsync(userClaims);
            return userClaims;
        }

        public async Task<IEnumerable<SelectionItem>> GetUserClaimSelectedList(Guid userId)
        {
            var list = await (from oc in Context.OperationClaims
                join userClaims in Context.UserClaims on oc.Id equals userClaims.ClaimId
                where userClaims.UserId == userId
                select new SelectionItem()
                {
                    Id = oc.Id.ToString(),
                    Label = oc.Name
                }).ToListAsync();

            return list;
        }
    }
}