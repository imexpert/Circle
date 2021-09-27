using Circle.Core.DataAccess.EntityFramework;
using Circle.Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class LogRepository : EntityRepositoryBase<Log, ProjectDbContext>, ILogRepository
    {
        public LogRepository(ProjectDbContext context)
            : base(context)
        {
        }
    }
}