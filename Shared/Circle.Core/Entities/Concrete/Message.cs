using System;

namespace Circle.Core.Entities.Concrete
{
    public class Message : AuditEntity
    {
        public Guid LanguageId { get; set; }
        public Language Language { get; set; }
        public int Code { get; set; }
        public string MessageDetail { get; set; }
    }
}
