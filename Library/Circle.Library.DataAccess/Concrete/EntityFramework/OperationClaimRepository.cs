using Circle.Core.DataAccess.EntityFramework;
using Circle.Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class OperationClaimRepository : EntityRepositoryBase<OperationClaim, ProjectDbContext>,
        IOperationClaimRepository
    {
        public OperationClaimRepository(ProjectDbContext context)
            : base(context)
        {
        }
    }
}