using System;

namespace Circle.Core.Entities.Concrete
{
    public class UserGroup : AuditEntity
    {
        public Guid GroupId { get; set; }
        public Guid UserId { get; set; }
        public Group Group { get; set; }
        public User User { get; set; }
    }
}