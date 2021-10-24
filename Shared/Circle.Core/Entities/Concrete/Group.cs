using System;

namespace Circle.Core.Entities.Concrete
{
    public class Group : AuditEntity
    {
        public Guid LanguageId { get; set; }
        public string GroupName { get; set; }
    }
}