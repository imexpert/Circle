using System;

namespace Circle.Core.Entities.Concrete
{
    public class UserGroup : AuditEntity
    {
        public Guid GroupId { get; set; }
        public Guid UserId { get; set; }
    }
}