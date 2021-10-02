using Circle.Core.DataAccess.EntityFramework;
using Circle.Core.Entities.Concrete;
using Circle.Library.DataAccess.Abstract;
using Circle.Library.DataAccess.Concrete.EntityFramework.Contexts;

namespace Circle.Library.DataAccess.Concrete.EntityFramework
{
    public class MessageRepository : EntityRepositoryBase<Message, ProjectDbContext>,
        IMessageRepository
    {
        public MessageRepository(ProjectDbContext context)
            : base(context)
        {
        }
    }
}